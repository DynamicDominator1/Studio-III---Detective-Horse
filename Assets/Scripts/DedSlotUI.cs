using UnityEngine;
using UnityEngine.UI;

public class DedSlotUI : MonoBehaviour
{
    public GameObject frame; 
    public Image cluePhoto; 
    public Button pinButton; 

    private ClueSlot clueSlot;
    private DedBoardUI boardUI;

    public void Setup(ClueSlot slot, DedBoardUI board)
    {
        clueSlot = slot;
        boardUI = board;

        RefreshVisual();

        pinButton.onClick.AddListener(OnClicked);
    }

    void OnClicked()
    {
        boardUI.OpenPicker(clueSlot, this);
    }

    public void RefreshVisual()
    {
        if (clueSlot.placedClue != null)
        {
            frame.SetActive(true);
            if (clueSlot.placedClue.itemImage != null)
            {
                cluePhoto.sprite = clueSlot.placedClue.itemImage;
            }
        }
        else
        {
            frame.SetActive(false);
        }
    }
}