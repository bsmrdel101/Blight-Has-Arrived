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
    while (canMove)
    {
      yield return new WaitForSeconds(0.2f);
      Transform target = GameObject.FindWithTag("Player").transform;
      if (!target) continue;
      // TODO: Raycast to see if a wall is in the way of shooting
      agent.SetDestination(target.position);
    }
    agent.SetDestination(transform.position);
    yield return new WaitForSeconds(0.2f);
  }
}
