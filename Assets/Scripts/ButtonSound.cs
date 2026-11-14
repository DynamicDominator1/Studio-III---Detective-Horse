using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))] 
public class ButtonSound : MonoBehaviour
{
    public AudioClip clickSound; 

    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(PlayButtonSound); // run PlayButtonSound on button press
    }

    void PlayButtonSound()
    {
        if (AudioManager.Instance != null) // skip if no audio
        {
            AudioManager.Instance.PlaySFX(clickSound); 
        }
    }
}