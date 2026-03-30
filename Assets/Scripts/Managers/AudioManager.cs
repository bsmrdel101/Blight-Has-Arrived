using UnityEngine;

public class AudioManager : MonoBehaviour
{
  public static AudioManager Instance { get; private set; }

  [Header("Sounds")]
  [SerializeField] private AudioSource audioSource;
  [SerializeField] private AudioClip soundtrack;


  private void Awake()
  {
    if (Instance != null && Instance != this)
    {
      Destroy(gameObject);
      return;
    }
    Instance = this;
    gameObject.transform.SetParent(null);
    DontDestroyOnLoad(gameObject);
  }

  private void Start()
  {
    if (soundtrack != null)
    {
      audioSource.clip = soundtrack;
      audioSource.loop = true;
      audioSource.Play();
    }
  }
}