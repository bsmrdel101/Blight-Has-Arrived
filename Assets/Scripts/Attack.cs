using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
  [Header("Properties")]
  [SerializeField] private int dmg;
  [SerializeField] private float atkCooldown = 1f;
  public float range = 1.2f;
  [HideInInspector] public float atkDuration;
  [HideInInspector] public bool canAttack = true;
  
  [Header("References")]
  [SerializeField] private Animator animator;
  [SerializeField] private AnimationClip attackClip;
  [SerializeField] private BoxCollider2D boxCol;
  [SerializeField] private SpriteRenderer spriteRenderer;
  [SerializeField] private Transform pivotPoint;
  [HideInInspector] public Transform target;
  private Camera playerCam;


  private void Start()
  {
    atkDuration = attackClip ? attackClip.length : 0.3f;
    if (target == null)
    {
      playerCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
  }

  private void Update()
  {
    HandleAttackRotation();
  }


  private void OnTriggerEnter2D(Collider2D col)
  {
    Entity entity = col.gameObject.GetComponent<Entity>();
    if (entity)
    {
      entity.TakeDmg(dmg);
    }
  }

  private void HandleAttackRotation()
  {
    Vector3 dir;

    if (target != null)
    {
      dir = target.position - pivotPoint.position;
    }
    else
    {
      if (!playerCam) return;
      Vector2 mousePos = Mouse.current.position.ReadValue();
      Vector3 worldMouse = playerCam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Mathf.Abs(playerCam.transform.position.z)));
      dir = worldMouse - pivotPoint.position;
    }

    dir.Normalize();

    float targetRot = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    Quaternion targetRotation = Quaternion.Euler(0, 0, targetRot);
    pivotPoint.rotation = Quaternion.Lerp(pivotPoint.rotation, targetRotation, 20f * Time.deltaTime);

    if (targetRot > 90f || targetRot < -90f)
      spriteRenderer.flipY = true;
    else
      spriteRenderer.flipY = false;
  }

  public void StartAttack()
  {
    if (!canAttack) return;
    StartCoroutine(AttackCooldown());
    StartCoroutine(StartAttackCoroutine());
  }

  private IEnumerator StartAttackCoroutine()
  {
    if (animator) animator.Play(gameObject.name, 0, 0f);
    if (attackClip) spriteRenderer.enabled = true;
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
