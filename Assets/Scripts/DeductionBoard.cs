using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class DeductionBoard : MonoBehaviour
{
    public List<ClueSlot> slots;
    public List<ClueConnection> connections;
    public InputAction placeClueAction; // temporary test key - places the currently selected clue into the currently selected slot
    public int selectedSlotIndex = 0; // which slot is "selected" for testing

    void Start()
    {
        placeClueAction.Enable();
    }

    void Update()
    {
        if (placeClueAction.WasPressedThisFrame())
        {
            TestPlaceSelectedClue();
        }
    }

    void TestPlaceSelectedClue()
    {
        if (InventoryManager.Instance.collectedClues.Count == 0) return;

        ClueData selectedClue = InventoryManager.Instance.collectedClues[InventoryManager.Instance.selectedClueIndex];
        ClueSlot targetSlot = slots[selectedSlotIndex];

        PlaceClue(targetSlot, selectedClue);
    }

    public bool IsBoardComplete()
    {
        foreach (ClueSlot slot in slots)
        {
            if (!slot.IsCorrect()) return false;
        }

        foreach (ClueConnection connection in connections)
        {
            if (!connection.isUnlocked) return false;
        }

        return true;
    }

    public void UnlockConnection(ClueConnection connection)
    {
        connection.isUnlocked = true;
        Debug.Log("Unlocked connection: " + connection.contextReason);
        CheckCompletion();
    }

    public void PlaceClue(ClueSlot slot, ClueData clue)
    {
        slot.placedClue = clue;
        Debug.Log("Placed " + clue.clueName + " in slot");
        CheckCompletion();
    }

    void CheckCompletion()
    {
        if (IsBoardComplete())
        {
            Debug.Log("Board Complete!");
        }
    }
}