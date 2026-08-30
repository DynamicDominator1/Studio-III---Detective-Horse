using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Deduction Board", menuName = "Detective Horse/Deduction Board")]
public class DeductionBoardData : ScriptableObject
{
    public string boardName; // board data title
    public bool isUnlocked; // is board avaliable to player

    public List<ClueSlot> slots; // slots needed on board
    public List<ClueConnection> connections; // connections on board
    public List<DeductionBoardData> boardsToUnlock; // boards unlocked upon completion

    // checks if board is correct
    public bool IsBoardComplete()
    {
        foreach (ClueSlot slot in slots)
        {
            if (!slot.IsCorrect()) return false; // incorrect clue
        }

        foreach (ClueConnection connection in connections)
        {
            if (!connection.isUnlocked) return false; // no connection
        }

        return true; // if above is met, board completed
    }
}