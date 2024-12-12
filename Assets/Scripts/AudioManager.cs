using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    [SerializeField] private AudioClip expl, shot, coinPickUp, buyHealth;
    [SerializeField] private AudioSource audioSource;

    public void playSound(string audio)
    {
        switch (audio)
        {
            case "expl":
                audioSource.PlayOneShot(expl);
                break;
            case "shot":
                audioSource.PlayOneShot(shot);
                break;
            case "coin":
                audioSource.PlayOneShot(coinPickUp);
                break;
            case "buyHealth":
                audioSource.PlayOneShot(buyHealth);
                break;
        }

    }
}
