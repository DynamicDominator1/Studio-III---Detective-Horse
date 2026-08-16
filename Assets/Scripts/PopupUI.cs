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
        gameObject.SetActive(false); // default off
    }

    public void ShowClue(ClueData clue)
    {
        gameObject.SetActive(true);

        if (clue.itemImage != null)
        {
            popupImage.sprite = clue.itemImage;
        }

        popupTitle.text = clue.clueName;
        popupDescription.text = clue.description;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}