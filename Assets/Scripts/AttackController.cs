using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
  [SerializeField] private List<Attack> attacks = new List<Attack>();
  private Attack selectedAttack;
  private bool isHoldingAttack = false;
  
  private InputSystem_Actions controls;


  private void Awake()
  {
    selectedAttack = attacks[0];
    controls = new InputSystem_Actions();
    controls.Player.Attack.started += (ctx) => isHoldingAttack = true;
    controls.Player.Attack.canceled += (ctx) => isHoldingAttack = false;
  }

  private void Update()
  {
    if (isHoldingAttack) selectedAttack.StartAttack();
  }

  private void OnEnable()
  {
    controls.Enable();
  }

  private void OnDisable()
  {
    controls.Disable();
  }


  public void OnHit(Entity entity, int dmg)
  {
    entity.TakeDmg(dmg);
  }
}
