using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
  [Header("Properties")]
  [SerializeField] private int dmg;
  [SerializeField] private float atkCooldown = 1f;
  private float atkDuration;
  private bool canAttack = true;
  
  [Header("References")]
  [SerializeField] private Animator anim;
  [SerializeField] private AnimationClip attackClip;
  [SerializeField] private BoxCollider2D boxCol;
  [SerializeField] private SpriteRenderer spriteRenderer;
  [SerializeField] private Transform pivotPoint;
  private AttackController attackController;
  private Camera playerCam;


  private void Start()
  {
    attackController = GetComponentInParent<AttackController>();
    atkDuration = attackClip.length;
    playerCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
  }

  private void Update()
  {
    HandleAttackRotation();
  }


  private void OnCollisionEnter2D(Collision2D col)
  {
    Entity entity = col.gameObject.GetComponent<Entity>();
    if (entity) attackController.OnHit(entity, dmg);
  }

  private void HandleAttackRotation()
  {
    Vector2 mousePos = Mouse.current.position.ReadValue();
    Vector3 worldMouse = playerCam.ScreenToWorldPoint(mousePos);
    Vector3 dir = worldMouse - pivotPoint.position;
    dir.Normalize();

    float targetRot = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    Quaternion target = Quaternion.Euler(0, 0, targetRot);
    pivotPoint.rotation = Quaternion.Lerp(pivotPoint.rotation, target, 20f * Time.deltaTime);

    if (targetRot > 90f || targetRot < -90f)
    {
      spriteRenderer.flipY = true;
    }
    else
    {
      spriteRenderer.flipY = false;
    }
  }

  public void StartAttack()
  {
    if (!canAttack) return;
    StartCoroutine(AttackCooldown());
    StartCoroutine(StartAttackCoroutine());
  }

  private IEnumerator StartAttackCoroutine()
  {
    anim.Play(gameObject.name, 0, 0f);
    spriteRenderer.enabled = true;
    boxCol.enabled = true;

    yield return new WaitForSeconds(atkDuration);

    spriteRenderer.enabled = false;
    boxCol.enabled = false;
  }

  private IEnumerator AttackCooldown()
  {
    canAttack = false;
    yield return new WaitForSeconds(atkCooldown);
    canAttack = true;
  }
}
