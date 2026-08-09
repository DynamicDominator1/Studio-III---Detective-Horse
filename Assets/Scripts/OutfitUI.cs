using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class OutfitUI : MonoBehaviour
{
    public Transform outfitGrid;
    public GameObject outfitSlotPrefab;
    public GameObject outfitPanel;
    public InputAction toggleOutfitAction;

    public GameObject outfitDetailPanel; 
    public Image outfitDetailImage;
    public TextMeshProUGUI outfitDetailLabel;
    public Button equipButton;

    private OutfitData selectedOutfit;

    void Start()
    {
        toggleOutfitAction.Enable();
        outfitPanel.SetActive(false);
        outfitDetailPanel.SetActive(false);

        equipButton.onClick.AddListener(EquipSelected);
    }

    void Update()
    {
        if (toggleOutfitAction.WasPressedThisFrame())
        {
            ToggleOutfitPanel();
        }
    }

    void ToggleOutfitPanel()
    {
        bool isOpening = !outfitPanel.activeSelf;

        if (isOpening)
        {
            UIStateManager.Instance.OpenPanel(outfitPanel);
            RefreshGrid();
        }
        else
        {
            UIStateManager.Instance.ClosePanel(outfitPanel);
        }
    }

    public void RefreshGrid()
    {
        foreach (Transform child in outfitGrid)
        {
            Destroy(child.gameObject);
        }

        CreateOutfitButton(OutfitManager.Instance.baseHorseFit);

        foreach (OutfitData outfit in OutfitManager.Instance.ownedOutfits)
        {
            CreateOutfitButton(outfit);
        }
    }

    void CreateOutfitButton(OutfitData outfit)
    {
        GameObject newSlot = Instantiate(outfitSlotPrefab, outfitGrid);
        TextMeshProUGUI label = newSlot.GetComponentInChildren<TextMeshProUGUI>();
        label.text = outfit.outfitName;

        Button button = newSlot.GetComponent<Button>();
        button.onClick.AddListener(() => ShowOutfitDetail(outfit));
    }

    void ShowOutfitDetail(OutfitData outfit)
    {
        selectedOutfit = outfit;
        outfitDetailPanel.SetActive(true);
        outfitDetailLabel.text = outfit.outfitName;

        
    }

    void EquipSelected()
    {
        if (selectedOutfit != null)
        {
            OutfitManager.Instance.EquipOutfit(selectedOutfit);
        }
    }

    public void OpenFromNav()
    {
        UIStateManager.Instance.OpenPanel(outfitPanel);
        RefreshGrid();
    }
}