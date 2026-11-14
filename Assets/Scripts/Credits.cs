using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public RectTransform creditsText; 
    public float scrollSpeed = 60f; 
    public string nextScene = "Title";

    private float startY; 
    private float endY; 

    void Start()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(creditsText); // update text height

        float screenHeight = creditsText.GetComponentInParent<Canvas>().GetComponent<RectTransform>().rect.height; // get canvas height
        float textHeight = creditsText.rect.height; // how tall the credits text is

        startY = -screenHeight; // text start with top at bottom of screen
        endY = textHeight; // text end with bottom at top of screen

        creditsText.anchoredPosition = new Vector2(creditsText.anchoredPosition.x, startY); // set text at startY
    }

    void Update()
    {
        creditsText.anchoredPosition += Vector2.up * scrollSpeed * Time.unscaledDeltaTime; // move text up

        bool finished = creditsText.anchoredPosition.y >= endY; // has credits ended
        bool skipped = Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame; // has player pressed esc

        if (finished || skipped)
        {
            SceneManager.LoadScene(nextScene); // go to title screen
        }
    }
}