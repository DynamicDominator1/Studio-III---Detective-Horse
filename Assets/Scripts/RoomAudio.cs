using UnityEngine;

public class RoomAudio : MonoBehaviour
{
    public AudioClip music; 
    public AudioClip ambience;

    void Start()
    {
        if (AudioManager.Instance == null) return; 

        AudioManager.Instance.PlayMusic(music); 
        AudioManager.Instance.PlayAmbience(ambience);
    }
}