using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMovement : MonoBehaviour
{
  [Header("Movement")]
  [SerializeField] private float moveSpeed = 6f;

  [Header("References")]
  [SerializeField] private Rigidbody2D rb;

  private InputSystem_Actions controls;
  private Vector2 moveInput;


  private void Awake()
  {
    controls = new InputSystem_Actions();
    controls.Player.Move.performed += OnMove;
    controls.Player.Move.canceled += OnMove;
  }

  private void OnEnable()
  {
    controls.Enable();
  }

  private void OnDisable()
  {
    controls.Disable();
  }

  private void OnMove(CallbackContext ctx)
  {
    moveInput = ctx.ReadValue<Vector2>();
  }

  private void FixedUpdate()
  {
    MovePlayer();
  }

  private void MovePlayer()
  {
    Vector2 movement = moveSpeed * Time.fixedDeltaTime * moveInput;
    rb.MovePosition(rb.position + movement);
  }
}