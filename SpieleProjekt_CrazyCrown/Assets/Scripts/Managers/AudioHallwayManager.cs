using UnityEngine;

public class AudioHallwayManager : MonoBehaviour
{
    public AudioClip hallwayClip; // Hier kannst du das AudioClip im Inspector zuweisen
    public float delayInSeconds = 3f; // Die Anzahl der Sekunden, bevor der Clip abgespielt wird

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (hallwayClip != null)
        {
            Invoke("PlayHallwayClip", delayInSeconds);
        }
        else
        {
            Debug.LogWarning("Kein AudioClip zugewiesen!");
        }
    }

    void PlayHallwayClip()
    {
        audioSource.clip = hallwayClip;
        audioSource.Play();
    }
}
