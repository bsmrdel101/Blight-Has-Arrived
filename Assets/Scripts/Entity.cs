using UnityEngine;

public abstract class Entity : MonoBehaviour
{
  [Header("Main Stats")]
  [SerializeField] protected int maxHp;
  protected int hp;

  [Header("Movement")]
  public float moveSpeed;
  public float acceleration;
  public float followDistance;

  
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

  protected abstract void OnDeath();
}
