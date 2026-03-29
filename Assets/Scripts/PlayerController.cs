using UnityEngine;

public class PlayerController : Entity
{
  override protected void OnDeath()
  {
    Destroy(gameObject);
  }
}
