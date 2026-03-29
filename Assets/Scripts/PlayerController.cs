using UnityEngine;

public class PlayerController : Entity
{
  override protected void OnDeath()
  {
    base.OnDeath();
    Destroy(gameObject);
  }
}
