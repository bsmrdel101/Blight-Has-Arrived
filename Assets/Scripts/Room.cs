using System.Collections;
using UnityEngine;

public class Room : MonoBehaviour
{
  [SerializeField] private int difficulty;
  [SerializeField] private int enemyCount;
  [SerializeField] private bool spawnEnemies = true;
  bool clearedRoom = false;
  
  [Header("References")]
  private GameManager gameManager;


  private void Start()
  {
    gameManager = FindAnyObjectByType<GameManager>();

    if (!spawnEnemies || gameManager.HasClearedRoom(this)) return;
    EnemyManager enemyManager = FindAnyObjectByType<EnemyManager>(); 
    Entity[] enemies = enemyManager.GetRandomEnemies(difficulty, enemyCount);
    foreach (Entity entity in enemies)
    {
      GameObject obj = Instantiate(entity.prefab, transform.position, Quaternion.identity, transform);
      StartCoroutine(ResetRotationNextFrame(obj));
    }
  }

  private void Update()
  {
    if (!clearedRoom && !AreEnemiesAlive())
    {
      clearedRoom = true;
      gameManager.ClearRoom(this);
    }
  }

  private IEnumerator ResetRotationNextFrame(GameObject obj)
  {
    yield return null;
    obj.transform.eulerAngles = Vector3.zero;
  }

  public bool AreEnemiesAlive()
  {
    return FindObjectsByType<EnemyAttackController>().Length > 0;
  }
}
