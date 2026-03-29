using System.Collections;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
  [SerializeField] private Attack attack;
  [SerializeField] private Animator animator;
  [SerializeField] private AnimationClip attackClip;


  private void Start()
  {
    StartCoroutine(HandleAttack());
  }


  private IEnumerator HandleAttack()
  {
    Transform player = GameObject.FindWithTag("Player").transform;
    attack.target = player;
    while(true)
    {
      float distance = Vector3.Distance(player.position, transform.position);
      if (distance <= attack.range && attack.canAttack)
      {
        if (attackClip) animator.Play(attackClip.name, 0, 0f);
        attack.StartAttack();
      }
      yield return new WaitForSeconds(attack.atkDuration);
    }
  }
}
