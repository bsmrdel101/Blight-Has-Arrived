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

  [Header("References")]
  [SerializeField] private Rigidbody2D rb;
  [SerializeField] private NavMeshAgent agent;
  public GameObject prefab;

  
  private void Start()
  {
    hp = maxHp;
  }


  public virtual void TakeDmg(int dmg)
  {
    hp -= dmg;
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

  private IEnumerator WaitForDeathAnim()
  {
    yield return new WaitForSeconds(deathAnim.length);
    Destroy(gameObject);
  }
}
