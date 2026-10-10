using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NotificationUI : MonoBehaviour
{
    public static NotificationUI Instance; // singleton so any script can call NotificationUI.Instance.Show(...)

    public GameObject panel; // the background panel that holds the text, drag it in
    public TMP_Text messageText; // the TextMeshPro text inside the panel, drag it in
    public Image notificationImage; // NEW: the Image inside the panel that shows the pickup's artwork, drag it in
    public float displayTime = 3f; // how many seconds the popup stays on screen

    private float timer; // counts down while the popup is showing

    void Awake()
    {
        Instance = this; // register this script as the one shared instance
    }

    void Start()
    {
        panel.SetActive(false); // start with the popup hidden
    }

    void Update()
    {
        if (timer > 0f) // only count down while a popup is showing
        {
            timer -= Time.deltaTime; // tick the timer down by the time since last frame

            if (timer <= 0f) // check whether the time has run out
            {
                panel.SetActive(false); // hide the popup again
            }
        }
    }

    public void Show(string message, Sprite icon = null) // CHANGED: icon is optional, so existing calls without one still work
    {
        messageText.text = message; // put the new message into the text box

        if (icon != null) // NEW: this pickup has artwork
        {
            notificationImage.sprite = icon; // NEW: put the artwork into the image
            notificationImage.gameObject.SetActive(true); // NEW: make sure the image is visible
        }
        else // NEW: no artwork yet
        {
            notificationImage.gameObject.SetActive(false); // NEW: hide the empty image slot
        }

        panel.SetActive(true); // make the panel visible
        timer = displayTime; // restart the countdown, so a new pickup resets the timer
    }
}
