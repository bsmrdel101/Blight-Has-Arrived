using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
  public void OnClick_Start()
  {
    SceneManager.LoadScene("Game");
  }
}
