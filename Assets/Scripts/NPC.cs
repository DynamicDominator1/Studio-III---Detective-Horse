using UnityEngine;
using System.Collections.Generic;

public class NPC : Interactable
{
    public bool ignoreOutfit; 
    public OutfitData requiredOutfit; // correct outfit to speak with NPC
    public string wrongOutfitLine = "No Horses!";
    public string greeting = "Yes?";
    public List<Dialogue> dialogueOptions; // base dialogue options

    private List<Dialogue> currentOptions; // dialogue optionbs being shown

    public override void Interact()
    {
        if (!ignoreOutfit && OutfitManager.Instance.currentOutfit != requiredOutfit) // outfit check skipped when ignoreOutfit ticked
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
        if (option.boardToUnlockOn != null && option.connectionIndexesToUnlock != null) 
        {
            foreach (int index in option.connectionIndexesToUnlock) 
            {
                if (index >= 0 && index < option.boardToUnlockOn.connections.Count) 
                {
                    ClueConnection connection = option.boardToUnlockOn.connections[index]; 
                    DeductionManager.Instance.UnlockConnection(connection); // sets connection active in ded board
                }
            }
        }

        if (option.boardToUnlock != null)
        {
            option.boardToUnlock.isUnlocked = true;

        }

       
        
            if (option.clueToGive != null && !InventoryManager.Instance.collectedClues.Contains(option.clueToGive)) // this option gives a clue
        {
                InventoryManager.Instance.AddClue(option.clueToGive); // add the clue to the inventory, same call the clue pickup uses
                NotificationUI.Instance.Show("Clue found: " + option.clueToGive.clueName, option.clueToGive.itemImage); // popup with the clue's name and artwork
            }

            if (option.outfitToGive != null && !OutfitManager.Instance.ownedOutfits.Contains(option.outfitToGive)) // this option gives an outfit
        {
                OutfitManager.Instance.AddOutfit(option.outfitToGive); // add the outfit to the players outfit inventory, same call the outfit pickup uses
                NotificationUI.Instance.Show("Outfit found: " + option.outfitToGive.outfitName, option.outfitToGive.itemImage); // popup with the outfit's name and artwork
            }

          
       

        if (option.objectsToRemove != null)
        {
            foreach (GameObject obj in option.objectsToRemove) // check each object
            {
                if (obj != null) 
                {
                    WorldStateManager.Remove(obj); //switch object off forever
                }
            }
        }

        currentOptions = (option.followUpOptions != null && option.followUpOptions.Count > 0)
            ? option.followUpOptions
            : dialogueOptions;

        DialogueUI.Instance.UpdateDialogue(option.npcResponse, currentOptions); // show response and next options
    }
}