using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupUI : MonoBehaviour
{
    public Image popupImage;
    public TextMeshProUGUI popupTitle;
    public TextMeshProUGUI popupDescription;

    void Start()
    {
        gameObject.SetActive(false); // starts hidden
    }

    public void ShowClue(ClueData clue)
    {
        gameObject.SetActive(true); // show popup

        if (clue.itemImage != null)
        {
            popupImage.sprite = clue.itemImage; // set image 
        }

        popupTitle.text = clue.clueName; // set title
        popupDescription.text = clue.description; // set desc
    }

    public void Hide()
    {
        gameObject.SetActive(false); // close panel
    }
}