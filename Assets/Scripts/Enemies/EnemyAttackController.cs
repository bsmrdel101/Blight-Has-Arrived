using System.Collections;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{ 
  [Header("Animations")]
  [SerializeField] private Animator animator;
  [SerializeField] private AnimationClip attackClip;

  [Header("References")]
  [SerializeField] private Attack attack;
  [SerializeField] private Entity entity;


  private void Start()
  {
    StartCoroutine(HandleAttack());
  }


  private IEnumerator HandleAttack()
  {
    Transform player = GameObject.FindWithTag("Player").transform;
    attack.target = player;
    float prevSpeed = entity.agent.speed;
    while(true)
    {
      if (!player) break;
      float distance = Vector3.Distance(player.position, transform.position);
      if (distance <= attack.range && attack.canAttack && !entity.isDead)
      {
        if (attackClip) animator.Play(attackClip.name, 0, 0f);
        entity.agent.speed = 0;
        attack.StartAttack();
      }
      yield return new WaitForSeconds(attack.atkDuration);
      entity.agent.speed = prevSpeed;
    }
  }
}
