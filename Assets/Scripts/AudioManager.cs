using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; 

    [Range(0f, 1f)] public float musicVolume = 0.5f; // music Volume
    [Range(0f, 1f)] public float ambienceVolume = 0.5f; // ambience volume
    [Range(0f, 1f)] public float sfxVolume = 1f; // sfx volume

    private AudioSource musicSource; 
    private AudioSource ambienceSource; 
    private AudioSource sfxSource; 

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // set the shared instance
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // ensure only 1 audio manager
            return;
        }

        musicSource = gameObject.AddComponent<AudioSource>(); // create music player on object
        musicSource.loop = true; // music loops
        musicSource.playOnAwake = false;

        ambienceSource = gameObject.AddComponent<AudioSource>(); // create ambience player on object
        ambienceSource.loop = true; // ambience loops
        ambienceSource.playOnAwake = false; 

        sfxSource = gameObject.AddComponent<AudioSource>(); // create sound effect player on object
        sfxSource.playOnAwake = false;
    }

    void Update()
    {
        musicSource.volume = musicVolume; // keep sources matched to the volume sliders
        ambienceSource.volume = ambienceVolume;
        sfxSource.volume = sfxVolume;
    }

   
    public void PlayMusic(AudioClip clip)
    {
        if (clip == null)
        {
            musicSource.Stop(); // stop music
            musicSource.clip = null; // forget music 
            return;
        }

        if (musicSource.clip == clip && musicSource.isPlaying) return; // dont restart the same loop

        musicSource.clip = clip; // swap track
        musicSource.Play(); // start track
    }

    
    public void PlayAmbience(AudioClip clip)
    {
        if (clip == null)
        {
            ambienceSource.Stop(); 
            ambienceSource.clip = null; 
            return;
        }

        if (ambienceSource.clip == clip && ambienceSource.isPlaying) return; 

        ambienceSource.clip = clip; 
        ambienceSource.Play(); 
    }

    
    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;

        sfxSource.PlayOneShot(clip); // plays sfx
    }
}