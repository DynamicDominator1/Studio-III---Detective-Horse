using UnityEngine;
using System.Collections;

public class UIFade : MonoBehaviour
{
    public static UIFade Instance; 

    public CanvasGroup group; 
    public float fadeTime = 1f;
    public float holdTime = 0.5f;

    void Awake()
    {
        Instance = this; // set this script as the one shared instance
        group.alpha = 0f; // start clear so game isn't black
        group.blocksRaycasts = false; // dont block clicks
    }

    
    public IEnumerator FadeOut()
    {
        group.blocksRaycasts = true; // block clicks 

        while (group.alpha < 1f) // set unclear
        {
            group.alpha += Time.unscaledDeltaTime / fadeTime; // raise alpha a little each frame, unscaled so a paused game still fades
            yield return null; // wait one frame before the next step
        }

        group.alpha = 1f; // make sure it ends exactly black
    }

    
    public IEnumerator FadeIn()
    {
        while (group.alpha > 0f) // make transparent
        {
            group.alpha -= Time.unscaledDeltaTime / fadeTime; // lower alpha each frame
            yield return null; // wait one frame before the next step
        }

        group.alpha = 0f; // ends transparent
        group.blocksRaycasts = false; // dont block clicks
    }
}