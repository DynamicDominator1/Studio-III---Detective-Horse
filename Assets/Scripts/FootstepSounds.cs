using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootstepSounds : MonoBehaviour
{
    public AudioClip footstepClip;

    public float movingThreshold = 0.5f; // walk speed threshold
    public float runSpeedThreshold = 8f; // run speed threshold
    public float walkPitch = 1f; // walk speed playback
    public float runPitch = 1.5f; // run speed playback
    public float maxValidSpeed = 30f; // ignore speeds over this

    private AudioSource source;
    private Vector3 lastPosition;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        source.clip = footstepClip;
        source.loop = true; // loop
        source.playOnAwake = false;
        lastPosition = transform.position;
    }

    void Update()
    {
        Vector3 movement = transform.position - lastPosition; // how far horse moved since last frame
        movement.y = 0f; // ignore gravity movement

        float speed = movement.magnitude / Time.deltaTime; // speed in delta time 
        lastPosition = transform.position; // remember position

        bool isMoving = speed > movingThreshold && speed < maxValidSpeed;

        if (AudioManager.Instance != null)
        {
            source.volume = AudioManager.Instance.sfxVolume; // set sfx volume
        }

        if (isMoving)
        {
            source.pitch = speed > runSpeedThreshold ? runPitch : walkPitch; // faster playback while running

   

            if (!source.isPlaying)
            {
                source.Play(); // start the footsteps
            }
        }
        else
        {
            if (source.isPlaying)
            {
                source.Stop(); // stop the footsteps
            }
        }
    }
}