using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public Transform clueGrid;
    public GameObject clueSlotPrefab;
    public GameObject inventoryPanel;
    public InputAction toggleInventoryAction;
    public PopupUI popupUI;

    void Start()
    {
        toggleInventoryAction.Enable();
        inventoryPanel.SetActive(false);
    }

    void Update()
    {
        if (toggleInventoryAction.WasPressedThisFrame())
        {
            ToggleInventory();
        }
    }

    void ToggleInventory()
    {
        bool isOpening = !inventoryPanel.activeSelf;

        if (isOpening)
        {
            UIStateManager.Instance.OpenPanel(inventoryPanel);
            RefreshGrid();
        }
        else
        {
            UIStateManager.Instance.ClosePanel(inventoryPanel);
        }
    }

    public void RefreshGrid()
    {
        foreach (Transform child in clueGrid)
        {
            Destroy(child.gameObject);
        }

        for (int i = InventoryManager.Instance.collectedClues.Count - 1; i >= 0; i--)
        {
            ClueData clue = InventoryManager.Instance.collectedClues[i];

            GameObject newSlot = Instantiate(clueSlotPrefab, clueGrid);
            TextMeshProUGUI label = newSlot.GetComponentInChildren<TextMeshProUGUI>();
            label.text = clue.clueName;

            Image slotImage = newSlot.GetComponent<Image>();
            if (clue.itemImage != null)
            {
                slotImage.sprite = clue.itemImage;
            }

           
            Button button = newSlot.GetComponent<Button>();
            button.onClick.AddListener(() => popupUI.ShowClue(clue));
        }
    }

    public void OpenFromNav()
    {
        UIStateManager.Instance.OpenPanel(inventoryPanel);
        RefreshGrid();
    }
}