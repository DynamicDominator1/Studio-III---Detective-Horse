using UnityEngine;

[System.Serializable]
public class ClueSlot
{
    public ClueData correctClue; // Correct Ded board Clue
    public ClueData placedClue;  // places clue on Ded board
    public Vector2 uiPosition;   // Clue slot Ded board position

    public bool IsCorrect()
    {
        return placedClue != null && placedClue == correctClue;
    }
}