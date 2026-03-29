using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
  [Header("Navigation")]
  [SerializeField] private NavMeshAgent agent;
  private readonly bool canMove = true;

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
    while (canMove)
    {
      yield return new WaitForSeconds(0.2f);
      GameObject player = GameObject.FindWithTag("Player");
      if (!player) break;
      Transform target = player.transform;
      spriteRenderer.flipX = target.position.x < transform.position.x;
      // TODO: Raycast to see if a wall is in the way of shooting
      agent.SetDestination(target.position);
    }
    agent.SetDestination(transform.position);
    agent.speed = 0;
    yield return new WaitForSeconds(0.2f);
    animator.SetBool("isRunning", false);
  }
}
