using UnityEngine;

public class Totem : MonoBehaviour
{
  [SerializeField] private GameObject totemContent;
  [SerializeField] private BoxCollider2D boxCollider;
  private Popup popup;


  private void Start()
  {
    popup = FindAnyObjectByType<Popup>();
  }

  private void OnEnable()
  {
    GameManager.ConsumeTotemAction += OnConsumeTotem;
  }
  
  private void OnDisable()
  {
    GameManager.ConsumeTotemAction -= OnConsumeTotem;
  }


  private void OnTriggerEnter2D(Collider2D col)
  {
    if (col.tag != "Player") return;
    popup.CreatePopup("Consume totem?", GameManager.ConsumeTotemAction);
  }

  private void OnConsumeTotem()
  {
    Destroy(totemContent);
    Destroy(boxCollider);
  }
}
