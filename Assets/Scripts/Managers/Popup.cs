using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
  [SerializeField] private GameObject popupPrefab;
  [SerializeField] private TextMeshProUGUI popupText;
  [SerializeField] private Button yesBtn;


  public void CreatePopup(string msg, Action onYes)
  {
    popupText.text = msg;
    popupPrefab.SetActive(true);
    yesBtn.onClick.RemoveAllListeners();
    
    yesBtn.onClick.AddListener(() =>
    {
      onYes.Invoke();
      popupPrefab.SetActive(false);
    });
  }
}
