using System.Collections;
using UnityEngine;

public class SpikeAttack : MonoBehaviour
{
  [SerializeField] private int dmg;
  [SerializeField] private AnimationClip animationClip;
  [SerializeField] private float timeBeforeActive = 2f;
  [SerializeField] private BoxCollider2D boxCollider2D;

  private void Start()
  {
    StartCoroutine(ActivateHitbox());
    Destroy(gameObject, animationClip.length);
  }

  private void OnTriggerEnter2D(Collider2D col)
  {
    if (col.gameObject.tag != "Player") return;
    Entity entity = col.GetComponent<Entity>();
    if (entity) entity.TakeDmg(dmg);
  }

  private IEnumerator ActivateHitbox()
  {
    yield return new WaitForSeconds(timeBeforeActive);
    boxCollider2D.enabled = true;
  }
}
