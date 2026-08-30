using UnityEngine;
using System.Collections.Generic;

public class NPC : Interactable
{
    public OutfitData requiredOutfit; // correct outfit to speak with NPC
    public string wrongOutfitLine = "No Horses!";
    public string greeting = "Yes?";
    public List<Dialogue> dialogueOptions; // base dialogue options

    private List<Dialogue> currentOptions; // dialogue optionbs being shown

    public override void Interact()
    {
        if (OutfitManager.Instance.currentOutfit != requiredOutfit) // wearing wrong outfit
        {
            DialogueUI.Instance.ShowDismissal(wrongOutfitLine); // show wrong outfit line
            return;
        }

        currentOptions = dialogueOptions; // start with base options
        DialogueUI.Instance.OpenDialogue(this, greeting, currentOptions); // start with greeting
    }

    // called when player chooses dialogue option
    public void ChooseOption(Dialogue option)
    {
        bool isUnlocked = option.requiredClue == null || InventoryManager.Instance.collectedClues.Contains(option.requiredClue); //can player pick option based on unlocked data
        if (!isUnlocked) return;

        // if option picked, UI closes
        if (option.isGoodbye)
        {
            DialogueUI.Instance.CloseDialogue();
            return;
        }

        
       // option unlocks connection
        if (option.boardToUnlockOn != null && option.connectionIndexToUnlock >= 0)
        {
            ClueConnection connection = option.boardToUnlockOn.connections[option.connectionIndexToUnlock];
            DeductionManager.Instance.UnlockConnection(connection); // sets connection active in ded board
        }

        
        currentOptions = (option.followUpOptions != null && option.followUpOptions.Count > 0)
            ? option.followUpOptions
            : dialogueOptions;

        DialogueUI.Instance.UpdateDialogue(option.npcResponse, currentOptions); // show response and next options
    }
}