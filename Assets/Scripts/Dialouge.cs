using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string optionText; // what the player is "asking" - shown as a choice
    public ClueData requiredClue; // leave empty for an always-available option
    public string npcResponse; // what the NPC says when this option is chosen
}