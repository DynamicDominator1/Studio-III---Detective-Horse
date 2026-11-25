using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class OutfitGraph
{
    public OutfitData outfit; // the outfit the player must be wearing
    public DialogueGraph graph; // the conversation this outfit gets
}

public class NPC : Interactable
{
    public bool ignoreOutfit;
    
    public string wrongOutfitLine = "No Horses!";

    public DialogueGraph graph; // default graph

    public List<OutfitGraph> outfitGraphs; // outfits NPC will talk to plus its graph

    public List<GameObject> removableObjects; // objects npc can remove

    private Dialogue startNode; // start node
    private List<Dialogue> currentOptions;

    public override void Interact()
    {
        PlayInteractSound(); 

        DialogueGraph chosenGraph = null; //

        if (ignoreOutfit) // use default graph
        {
            chosenGraph = graph;
        }

        if (!ignoreOutfit) // match worn outfit to graph
        {
            foreach (OutfitGraph pair in outfitGraphs) 
            {
                if (pair.outfit == OutfitManager.Instance.currentOutfit) //outfit matche
                {
                    chosenGraph = pair.graph; // use its graph

                    break;
                }
            }
        }

        if (chosenGraph == null && !ignoreOutfit) // no outfit match
        {
            DialogueUI.Instance.ShowDismissal(wrongOutfitLine); // show wrong outfit line
            return;
        }

        if (chosenGraph == null)
        {
            Debug.LogError(name + " no dialogue graph");
            return;
        }

        startNode = chosenGraph.GetStartNode(); // find start node

        if (startNode == null) return; // log error

        currentOptions = startNode.GetFollowUps(); // display start node output options
        DialogueUI.Instance.OpenDialogue(this, startNode.npcResponse, currentOptions); // npc greeting
    }

    // called when player chooses dialogue option
    public void ChooseOption(Dialogue option) // takes node
    {
        bool isUnlocked = option.requiredClue == null || InventoryManager.Instance.collectedClues.Contains(option.requiredClue); // can player pick option based on unlocked data
        if (!isUnlocked) return;

        // option picked, UI close
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
            DeductionManager.Instance.UnlockBoard(option.boardToUnlock); 

        }

        if (option.clueToGive != null && !InventoryManager.Instance.collectedClues.Contains(option.clueToGive)) // give clue
        {
            InventoryManager.Instance.AddClue(option.clueToGive); // add clue to inventory
            NotificationUI.Instance.Show("Clue found: " + option.clueToGive.clueName, option.clueToGive.itemImage); // popup
        }

        if (option.outfitToGive != null && !OutfitManager.Instance.ownedOutfits.Contains(option.outfitToGive)) //  give outfit
        {
            OutfitManager.Instance.AddOutfit(option.outfitToGive); // add outfit to inventory
            NotificationUI.Instance.Show("Outfit found: " + option.outfitToGive.outfitName, option.outfitToGive.itemImage); // popup
        }



        if (option.objectIndexesToRemove != null) //remove objects
        {
            foreach (int index in option.objectIndexesToRemove) // check each number
            {
                if (index >= 0 && index < removableObjects.Count)
                {
                    if (removableObjects[index] != null)
                    {
                        WorldStateManager.Remove(removableObjects[index]); // switch object off forever
                    }
                }
            }
        }

        List<Dialogue> followUps = option.GetFollowUps(); // all outputs

        if (followUps.Count > 0)
        {
            currentOptions = followUps;
        }

        if (followUps.Count == 0)
        {
            currentOptions = startNode.GetFollowUps();
        }

        DialogueUI.Instance.UpdateDialogue(option.npcResponse, currentOptions); // show response and next options
    }
}