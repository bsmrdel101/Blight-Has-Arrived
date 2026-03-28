using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Creature", menuName = "Scriptable Objects/Creature")]
public class Creature : ScriptableObject
{
  public GameObject prefab;
  public int maxHp;
  public string description;
}
