using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Dialogue
{
    public string optionText;
    public ClueData requiredClue;
    public string npcResponse;

    
    public DeductionBoardData boardToUnlockOn;
    public int connectionIndexToUnlock = -1;

    public List<Dialogue> followUpOptions;
    public bool isGoodbye;
}