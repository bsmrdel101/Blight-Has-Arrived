using UnityEngine;
using UnityEngine.UI;

public class PlayerController : Entity
{
  [SerializeField] private Slider hpBar;


  override protected void OnDeath()
  {
    base.OnDeath();
    Destroy(gameObject);
  }

  override public void TakeDmg(int dmg)
  {
    base.TakeDmg(dmg);
    hpBar.value -= dmg;
  }
}
