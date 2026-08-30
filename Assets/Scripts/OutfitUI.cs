using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class OutfitUI : MonoBehaviour
{
    public Transform outfitGrid; // outfit slot parent
    public GameObject outfitSlotPrefab; // 
    public GameObject outfitPanel;  // UI for outfit panel
    public InputAction toggleOutfitAction; // ui panel open close input

    public GameObject outfitDetailPanel; 
    public Image outfitDetailImage;
    public TextMeshProUGUI outfitDetailLabel;
    public Button equipButton;

    private OutfitData selectedOutfit; // oufit selected in ui

    void Start()
    {
        toggleOutfitAction.Enable(); // allows opne close of ui
        outfitPanel.SetActive(false); // starts hidden
        outfitDetailPanel.SetActive(false); // popup starts closed

        equipButton.onClick.AddListener(EquipSelected); // equip button = selected outfit
    }

    void Update()
    {
        if (toggleOutfitAction.WasPressedThisFrame())
        {
            ToggleOutfitPanel(); //open ui
        }
    }

    void ToggleOutfitPanel()
    {
        bool isOpening = !outfitPanel.activeSelf; // open or closing

        if (isOpening)
        {
            UIStateManager.Instance.OpenPanel(outfitPanel); // open
            RefreshGrid(); // display correct info
        }
        else
        {
            UIStateManager.Instance.ClosePanel(outfitPanel); // close
        }
    }

    public void RefreshGrid()
    {
        // remove old info
        foreach (Transform child in outfitGrid)
        {
            Destroy(child.gameObject);
        }

        CreateOutfitButton(OutfitManager.Instance.baseHorseFit); // always show base outfit button

        foreach (OutfitData outfit in OutfitManager.Instance.ownedOutfits)
        {
            CreateOutfitButton(outfit); // create button for each outfit unlocked
        }
    }

    void CreateOutfitButton(OutfitData outfit)
    {
        GameObject newSlot = Instantiate(outfitSlotPrefab, outfitGrid);
        TextMeshProUGUI label = newSlot.GetComponentInChildren<TextMeshProUGUI>();
        label.text = outfit.outfitName; // set name

        Button button = newSlot.GetComponent<Button>();
        button.onClick.AddListener(() => ShowOutfitDetail(outfit)); // clicking shows detail panel
    }

    void ShowOutfitDetail(OutfitData outfit)
    {
        selectedOutfit = outfit; // selected outfit
        outfitDetailPanel.SetActive(true); // popup
        outfitDetailLabel.text = outfit.outfitName; // set popup text

        
    }

    void EquipSelected()
    {
        if (selectedOutfit != null) // outfit selected
        {
            OutfitManager.Instance.EquipOutfit(selectedOutfit); // equip
        }
    }

    // //open outfit ui from menu button
    public void OpenFromNav()
    {
        UIStateManager.Instance.OpenPanel(outfitPanel);
        RefreshGrid();
    }
}