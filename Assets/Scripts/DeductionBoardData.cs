using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Deduction Board", menuName = "Detective Horse/Deduction Board")]
public class DeductionBoardData : ScriptableObject
{
    public string boardName;
    public bool isUnlocked;
    public List<ClueSlot> slots;
    public List<ClueConnection> connections;
    public List<DeductionBoardData> boardsToUnlock; 

    public bool IsBoardComplete()
    {
        foreach (ClueSlot slot in slots)
        {
            if (!slot.IsCorrect()) return false;
        }

        foreach (ClueConnection connection in connections)
        {
            if (!connection.isUnlocked) return false;
        }

        return true;
    }
}