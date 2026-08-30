using UnityEngine;
using UnityEngine.UI;

public class DedSlotUI : MonoBehaviour
{
    public GameObject frame; // hidden until clue placed 
    public Image cluePhoto;
    public Button pinButton; // opens clue picker

    private ClueSlot clueSlot; // clue data for clue slot
    private DedBoardUI boardUI; 

    public void Setup(ClueSlot slot, DedBoardUI board)
    {
        clueSlot = slot; // slot data stored
        boardUI = board; // parent board stored

        RefreshVisual(); // show correct visuals

        pinButton.onClick.AddListener(OnClicked); // open picker when clicked
        
    }

    void OnClicked()
    {
        boardUI.OpenPicker(clueSlot, this);

    }

    public void RefreshVisual()
    {
        if (clueSlot.placedClue != null) // if clue slot is not empty
        {
            frame.SetActive(true); // show frame
            if (clueSlot.placedClue.itemImage != null)
            {
                cluePhoto.sprite = clueSlot.placedClue.itemImage; // update image
            }

        }
        else
        {
            frame.SetActive(false); // else hide frame
        }
    }
}