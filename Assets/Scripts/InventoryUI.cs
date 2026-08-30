using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public Transform clueGrid; // parent of clue slot visuals
    public GameObject clueSlotPrefab; 
    public GameObject inventoryPanel;
    public InputAction toggleInventoryAction; // open close input
    public PopupUI popupUI;

    void Start()
    {
        toggleInventoryAction.Enable(); // enable open close action
        inventoryPanel.SetActive(false); // hide panel
    }

    void Update()
    {
        // if toggleInventoryAction is pressed, open inventory UI
        if (toggleInventoryAction.WasPressedThisFrame()) 
        {
            ToggleInventory(); 
        }
    }

    void ToggleInventory()
    {
        bool isOpening = !inventoryPanel.activeSelf; // open or close

        if (isOpening)
        {
            UIStateManager.Instance.OpenPanel(inventoryPanel); // open
            RefreshGrid(); // display correct clue data
        }
        else
        {
            UIStateManager.Instance.ClosePanel(inventoryPanel); // close
        }
    }

    public void RefreshGrid()
    {
        foreach (Transform child in clueGrid)
        {
            Destroy(child.gameObject);
        }

        // new clues show up first
        for (int i = InventoryManager.Instance.collectedClues.Count - 1; i >= 0; i--)
        {
            ClueData clue = InventoryManager.Instance.collectedClues[i];

            GameObject newSlot = Instantiate(clueSlotPrefab, clueGrid);
            TextMeshProUGUI label = newSlot.GetComponentInChildren<TextMeshProUGUI>();
            label.text = clue.clueName;

            Image slotImage = newSlot.GetComponent<Image>();
            if (clue.itemImage != null)
            {
                slotImage.sprite = clue.itemImage; // icon is clue image
            }

           
            Button button = newSlot.GetComponent<Button>();
            button.onClick.AddListener(() => popupUI.ShowClue(clue)); // click opens popup
        }
    }

    //open inventory from menu button
    public void OpenFromNav()
    {
        UIStateManager.Instance.OpenPanel(inventoryPanel);
        RefreshGrid();
    }
}