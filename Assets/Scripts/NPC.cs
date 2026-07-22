using UnityEngine;
using System.Collections.Generic;

public class NPC : Interactable
{
    public OutfitData requiredOutfit; // the outfit the player needs to be wearing to talk properly
    public string wrongOutfitLine = "No Horses!"; // said when player isn't disguised correctly
    public List<Dialogue> dialogueOptions; // each entry is one thing the player can ask about
    public string greeting = "Yes?";

    public override void Interact()
    {
        if (OutfitManager.Instance.currentOutfit != requiredOutfit)
        {
            Debug.Log(interactionName + ": " + wrongOutfitLine);
            return; // stop here - don't let the player talk further if the outfit's wrong
        }

        Debug.Log(interactionName + ": " + greeting);
    }
}