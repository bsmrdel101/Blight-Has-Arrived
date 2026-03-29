using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
  [Header("Navigation")]
  [SerializeField] private NavMeshAgent agent;

  [Header("References")]
  [SerializeField] private Entity entity;
  [SerializeField] private Animator animator;
  [SerializeField] private SpriteRenderer spriteRenderer;


  private void Start()
  {
    agent.speed = entity.moveSpeed;
    agent.acceleration = entity.acceleration;
    agent.updateRotation = false;
    agent.updateUpAxis = false;
    agent.stoppingDistance = entity.followDistance;
    StartCoroutine(UpdateTarget());
  }


  private IEnumerator UpdateTarget()
  {
    animator.SetBool("isRunning", true);
    while (!entity.isDead)
    {
      yield return new WaitForSeconds(0.2f);
      GameObject player = GameObject.FindWithTag("Player");
      if (!player || entity.isDead) break;
      Transform target = player.transform;
      spriteRenderer.flipX = target.position.x < transform.position.x;
      agent.SetDestination(target.position);
    }
    if (!entity.isDead)
    {
      agent.SetDestination(transform.position);
      agent.speed = 0;
      yield return new WaitForSeconds(0.2f);
      animator.SetBool("isRunning", false);
    }
  }
}
