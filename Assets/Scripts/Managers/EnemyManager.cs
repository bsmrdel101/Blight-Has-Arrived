using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
  [SerializeField] private List<EnemyListItem> enemies = new List<EnemyListItem>();


  public Entity[] GetRandomEnemies(int difficulty, int amount)
  {
    var filteredEnemies = enemies.Where((e) => e.difficulty == difficulty).ToArray();
    List<Entity> entities = new List<Entity>();

    for (int i = 0; i < amount; i++)
    {
      int randomIndex = UnityEngine.Random.Range(0, filteredEnemies.Length);
      entities.Add(filteredEnemies[randomIndex].entity);
    }

    return entities.ToArray();
  }
}

[Serializable]
public struct EnemyListItem
{
  public int difficulty;
  public Entity entity;

  public EnemyListItem(int difficulty, Entity entity)
  {
    this.difficulty = difficulty;
    this.entity = entity;
  }
}
