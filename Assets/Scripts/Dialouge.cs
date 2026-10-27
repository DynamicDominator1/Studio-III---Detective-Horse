using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Dialogue
{
    public string optionText; // text shown on button
    public ClueData requiredClue; // needed clue to unlock button
    public DeductionBoardData requiredBoard; // required completed ded board
    public string npcResponse;


    public DeductionBoardData boardToUnlockOn;
    public List<int> connectionIndexesToUnlock;

    public DeductionBoardData boardToUnlock;

    public ClueData clueToGive; 
    public OutfitData outfitToGive; 
    [HideInInspector] public bool itemGiven; // sets item as given

    public List<GameObject> objectsToRemove; 

    public List<Dialogue> followUpOptions;
    public bool isGoodbye; // does picking option end interaction
}