using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class NotificationUI : MonoBehaviour
{
    public static NotificationUI Instance; // singleton

    public GameObject panel; // background panel
    public TMP_Text messageText; // TextMeshPro text 
    public Image notificationImage; //Image inside the panel 
    public float displayTime = 3f; // seconds the popup stays

    private float timer; // counts down while the popup is showing

    private bool isHint; // true while tutorial hint on screen
    private Key hintDismissKey; // key to remove hint popup
    private int hintShownFrame;

    void Awake()
    {
        Instance = this; // register this script as the one shared instance
    }

    void Start()
    {
        panel.SetActive(false); // start with popup hidden
    }

    void Update()
    {
        if (isHint) // hints ignore timer
        {
            bool keyPressed = Keyboard.current != null && Keyboard.current[hintDismissKey].wasPressedThisFrame; // hint's dismiss key pressed
            bool shownEarlier = Time.frameCount > hintShownFrame; // hint on screen for longer than 1 frame

            if (keyPressed && shownEarlier) 
            {
                isHint = false; // set hint done
                panel.SetActive(false); // hinde hint
            }

            return;
        }

        if (timer > 0f) // only count down while a popup is showing
        {
            timer -= Time.deltaTime; // tick timer down by time since last frame

            if (timer <= 0f) // check if time has ended
            {
                panel.SetActive(false); // hide popup again
            }
        }
    }

    public void Show(string message, Sprite icon = null) //icon is optional
    {
        isHint = false;

        messageText.text = message; // put message into text box

        if (icon != null)
        {
            notificationImage.sprite = icon; // sprite into image
            notificationImage.gameObject.SetActive(true); // make image visible
        }
        else //no sprites yet
        {
            notificationImage.gameObject.SetActive(false); // hide image slot
        }

        panel.SetActive(true); // make panel visible
        timer = displayTime; // restart the countdown
    }

    
    public void ShowHint(string message, Sprite icon, Key dismissKey)
    {
        Show(message, icon); // fill in text and image

        if (dismissKey == Key.None) return; // no key set sets timer

        isHint = true; // wait for key
        hintDismissKey = dismissKey; 
        hintShownFrame = Time.frameCount; 
        timer = 0f; // cancel timer
    }
}