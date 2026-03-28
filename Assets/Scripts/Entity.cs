using UnityEngine;

public abstract class Entity : MonoBehaviour
{
  [SerializeField] protected int maxHp;
  protected int hp;


  protected abstract void TakeDmg();

  protected abstract void OnDeath();
}
