using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : Entity
{
  [Header("References")]
  [SerializeField] private Slider hpBar;
  [SerializeField] private AudioSource audioSource;
  [SerializeField] private AudioClip hurtSound;


  override protected void OnDeath()
  {
    base.OnDeath();
    SceneManager.LoadScene("GameOver");
  }

  override public void TakeDmg(int dmg)
  {
    base.TakeDmg(dmg);
    hpBar.value -= dmg;
    audioSource.PlayOneShot(hurtSound);
  }
}
