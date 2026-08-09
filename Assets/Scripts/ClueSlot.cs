using UnityEngine;

[System.Serializable]
public class ClueSlot
{
    public ClueData correctClue; 
    public ClueData placedClue;
    public Vector2 uiPosition;

    public bool IsCorrect()
    {
        return placedClue != null && placedClue == correctClue;
    }
}