using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public abstract class Entity : MonoBehaviour
{
  [Header("Main Stats")]
  [SerializeField] protected int maxHp;
  protected int hp;
  [HideInInspector] public bool isDead = false;

  [Header("Movement")]
  public float moveSpeed;
  public float acceleration;
  public float followDistance;

  [Header("Animations")]
  [SerializeField] private Animator animator;
  [SerializeField] private AnimationClip deathAnim;
  [SerializeField] private Color defaultColor = Color.white;
  [SerializeField] private Color takeDmgColor;

  [Header("References")]
  [SerializeField] private Rigidbody2D rb;
  [SerializeField] private SpriteRenderer spriteRenderer;
  public NavMeshAgent agent;
  public GameObject prefab;

  
  private void Start()
  {
    hp = maxHp;
  }


  public virtual void TakeDmg(int dmg)
  {
    hp -= dmg;
    StartCoroutine(FlashHitColor());
    if (hp <= 0)
    {
      OnDeath();
    }
  }

  protected virtual void OnDeath()
  {
    if (isDead) return;
    isDead = true;

    if (rb)
    {
      rb.linearVelocity = Vector2.zero;
      rb.simulated = false;  
    }
    if (agent) agent.enabled = false;

    if (deathAnim)
    {
      animator.SetTrigger("death");
      StartCoroutine(WaitForDeathAnim());
    }
  }

  private IEnumerator FlashHitColor()
  {
    spriteRenderer.color = takeDmgColor;
    yield return new WaitForSeconds(0.1f);
    spriteRenderer.color = defaultColor;
  }

  private IEnumerator WaitForDeathAnim()
  {
    yield return new WaitForSeconds(deathAnim.length);
    Destroy(gameObject);
  }
}
