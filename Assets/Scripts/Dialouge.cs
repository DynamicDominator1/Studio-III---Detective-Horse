using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Dialogue
{
    public string optionText; // text shown on button
    public ClueData requiredClue; // needed clue to unlock button
    public string npcResponse;

    
    public DeductionBoardData boardToUnlockOn; 
    public int connectionIndexToUnlock = -1;

    public List<Dialogue> followUpOptions;
    public bool isGoodbye; // does picking option end interaction
}