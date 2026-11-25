using UnityEngine;
using System.Collections.Generic;
using XNode;

// Empty type, only used so two sockets can be plugged together
[System.Serializable]
public class DialogueLink
{
}

[NodeWidth(400)] // Node size

public class Dialogue : Node
{
    [Input] public DialogueLink inLine; // line entry
    [Output] public DialogueLink outLine; // line exit

    public bool isStart; // tick for start

    public string optionText; // text shown on button
    public ClueData requiredClue; // needed clue to unlock button
    public DeductionBoardData requiredBoard; // required completed ded board
    public string npcResponse; // what the NPC says after this is picked

    public DeductionBoardData boardToUnlockOn; // board that gets connections unlocked
    public List<int> connectionIndexesToUnlock; // which connections

    public DeductionBoardData boardToUnlock; // board unlocked

    public ClueData clueToGive; // clue given to the player
    public OutfitData outfitToGive; // outfit given to the player
    public List<int> objectIndexesToRemove;

    public bool isGoodbye; // end interaction


    public override object GetValue(NodePort port)
    {
        return null; 
    }

    // gives back every box this one has a line drawn to
    public List<Dialogue> GetFollowUps()
    {
        List<Dialogue> result = new List<Dialogue>(); // list

        NodePort port = GetOutputPort("outLine"); // output socket

        foreach (NodePort connection in port.GetConnections()) // every line coming out
        {
            Dialogue next = connection.node as Dialogue; // the node on the other end

            if (next != null) 
            {
                result.Add(next); // add to follow ups
            }
        }

        return result; 
    }
}