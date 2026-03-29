using UnityEngine;

public class RoomTransition : MonoBehaviour
{
  [SerializeField] private Room room;
  [SerializeField] private GameObject roomPrefab;
  [SerializeField] private string sideOutput;
  private bool isActive = false;


  private void Update()
  {
    if (!room.AreEnemiesAlive()) isActive = true;
  }


  private void OnTriggerEnter2D(Collider2D col)
  {
    if (!isActive || col.tag != "Player") return;
    Destroy(room.gameObject);
    Instantiate(roomPrefab, Vector3.zero, Quaternion.identity);
    GameObject obj = GameObject.FindWithTag(sideOutput);
    col.transform.position = obj.transform.position;
  }
}
