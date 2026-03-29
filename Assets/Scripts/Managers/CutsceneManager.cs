using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CutsceneManager : MonoBehaviour
{
  [Header("Cutscene")]
  [SerializeField] private GameObject cutsceneCanvas;
  [SerializeField] private Cutscene openingCutscene;
  [SerializeField] private bool skipOpening = false;
  private Cutscene selectedCutscene;
  private int index = 0;

  [Header("Sound")]
  [SerializeField] private AudioSource audioSource;
  [SerializeField] private AudioClip transitionSound;

  [Header("References")]
  [SerializeField] private Transform textContainer;
  [SerializeField] private GameObject textLinePrefab;

  private InputAction continueAction;


  private void Awake()
  {
    continueAction = new InputAction(type: InputActionType.Button);
    continueAction.AddBinding("<Keyboard>/anyKey");
    continueAction.AddBinding("<Mouse>/leftButton");
    continueAction.performed += ctx => OnContinue();
  }

  private void Start()
  {
    if (!skipOpening) Play(openingCutscene);
  }

  private void OnEnable()
  {
    continueAction.Enable();
  }

  private void OnDisable()
  {
    continueAction.Disable();
  }


  private void OnContinue()
  {
    if (selectedCutscene == null) return;
    DisplayNextLine(selectedCutscene);
  }

  public void Play(Cutscene cutscene)
  {
    index = 0;
    selectedCutscene = cutscene;
    cutsceneCanvas.SetActive(true);
    DisplayNextLine(cutscene);
  }

  private void DisplayNextLine(Cutscene cutscene)
  {
    if (index >= cutscene.textList.Length)
    {
      cutsceneCanvas.SetActive(false);
      return;
    }

    GameObject lineObj = Instantiate(textLinePrefab, Vector2.zero, Quaternion.identity);
    TextMeshProUGUI lineText = lineObj.GetComponent<TextMeshProUGUI>();
    lineObj.transform.SetParent(textContainer, false);
    lineText.text = cutscene.textList[index];
    index += 1;
    // audioSource.PlayOneShot(transitionSound);
  }
}
