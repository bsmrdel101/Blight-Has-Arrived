using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  private int totemsConsumed = 0;
  private HashSet<string> clearedRooms = new HashSet<string>();

  [Header("Unity Actions")]
  public static Action ConsumeTotemAction;

  [Header("References")]
  [SerializeField] private CutsceneManager cutsceneManager;
  [SerializeField] private Cutscene firstTotemCutscene;
  [SerializeField] private TextMeshProUGUI energyTrackerText;


  private void OnEnable()
  {
    ConsumeTotemAction += OnConsumeTotem;
  }
  
  private void OnDisable()
  {
    ConsumeTotemAction -= OnConsumeTotem;
  }


  private void OnConsumeTotem()
  {
    totemsConsumed += 1;
    energyTrackerText.text = $"{totemsConsumed} / 3";
    if (totemsConsumed == 1)
    {
      cutsceneManager.Play(firstTotemCutscene);
    } else if (totemsConsumed == 3)
    {
      SceneManager.LoadScene("BadEnding");
    }
  }

  public bool HasClearedRoom(Room room)
  {
    return clearedRooms.Contains(room.name);
  }

  public void ClearRoom(Room room)
  {
    clearedRooms.Add(room.name);
  }
}
