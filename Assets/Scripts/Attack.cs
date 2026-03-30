using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public enum AttackType
{
  Melee,
  Ranged,
  RangedSpawn
}

public class Attack : MonoBehaviour
{
  [Header("Properties")]
  [SerializeField] private int dmg;
  [SerializeField] private float atkCooldown = 1f;
  public float range = 1.2f;
  [HideInInspector] public float atkDuration;
  [HideInInspector] public bool canAttack = true;
  [SerializeField] private AttackType attackType;
  
  [Header("References")]
  [SerializeField] private AudioSource audioSource;
  [SerializeField] private AudioClip atkSound;
  [SerializeField] private Animator animator;
  [SerializeField] private AnimationClip attackClip;
  [SerializeField] private BoxCollider2D boxCol;
  [SerializeField] private SpriteRenderer spriteRenderer;
  [SerializeField] private Transform pivotPoint;
  [HideInInspector] public Transform target;
  [SerializeField] private GameObject spawnAtkPrefab;
  private Camera playerCam;


  private void Start()
  {
    atkDuration = attackClip ? attackClip.length : 0.3f;
    if (!target)
    {
      playerCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
  }

  private void Update()
  {
    if (attackType == AttackType.Melee)
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

  private Vector3 GetSpawnPosition()
  {
    if (target != null)
    {
      return target.position + new Vector3(0, 2f, 0);
    }
    return transform.position;
  }

  public void StartAttack()
  {
    if (!canAttack) return;
    if (!target) audioSource.PlayOneShot(atkSound);
    StartCoroutine(AttackCooldown());
    StartCoroutine(StartAttackCoroutine());
  }

  private IEnumerator StartAttackCoroutine()
  {
    if (animator) animator.Play(gameObject.name, 0, 0f);

    switch (attackType)
    {
      case AttackType.Melee:
        DoMeleeAttack();
        break;

      case AttackType.RangedSpawn:
        DoRangedSpawnAttack();
        break;
    }

    yield return new WaitForSeconds(atkDuration);

    EndAttack();
  }

  private void DoMeleeAttack()
  {
    if (attackClip) spriteRenderer.enabled = true;
    boxCol.enabled = true;
  }

  private void DoRangedSpawnAttack()
  {
    Vector3 spawnPos = GetSpawnPosition();
    Instantiate(spawnAtkPrefab, spawnPos, Quaternion.identity);
  }

  private void EndAttack()
  {
    if (attackType != AttackType.Melee) return;
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
