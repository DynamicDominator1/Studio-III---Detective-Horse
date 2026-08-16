using UnityEngine;
using System.Collections.Generic;

public class NPC : Interactable
{
    public OutfitData requiredOutfit;
    public string wrongOutfitLine = "No Horses!";
    public string greeting = "Yes?";
    public List<Dialogue> dialogueOptions;

    private List<Dialogue> currentOptions;

    public override void Interact()
    {
        if (OutfitManager.Instance.currentOutfit != requiredOutfit)
        {
            DialogueUI.Instance.ShowDismissal(wrongOutfitLine);
            return;
        }

        currentOptions = dialogueOptions;
        DialogueUI.Instance.OpenDialogue(this, greeting, currentOptions);
    }

    public void ChooseOption(Dialogue option)
    {
        bool isUnlocked = option.requiredClue == null || InventoryManager.Instance.collectedClues.Contains(option.requiredClue);
        if (!isUnlocked) return;

        if (option.isGoodbye)
        {
            DialogueUI.Instance.CloseDialogue();
            return;
        }

        // dialouge option unlocks string connector on ded board
        if (option.boardToUnlockOn != null && option.connectionIndexToUnlock >= 0)
        {
            ClueConnection connection = option.boardToUnlockOn.connections[option.connectionIndexToUnlock];
            DeductionManager.Instance.UnlockConnection(connection);
        }

        
        currentOptions = (option.followUpOptions != null && option.followUpOptions.Count > 0)
            ? option.followUpOptions
            : dialogueOptions;

        DialogueUI.Instance.UpdateDialogue(option.npcResponse, currentOptions);
    }
}