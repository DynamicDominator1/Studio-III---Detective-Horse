using UnityEngine;
using System.Collections.Generic;

public class DeductionManager : MonoBehaviour
{
    public static DeductionManager Instance;

    public List<DeductionBoardData> allBoards; // all game boards
    private int currentBoardIndex = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

#if UNITY_EDITOR
        ResetAllBoardsForTesting();
#endif

    }

    // resets references within unity for deduction board before game compiles
    void ResetAllBoardsForTesting()
    {
        for (int i = 0; i < allBoards.Count; i++)
        {
            DeductionBoardData board = allBoards[i];
            board.isUnlocked = (i == 0);

            foreach (ClueSlot slot in board.slots)
            {
                slot.placedClue = null;
            }
            foreach (ClueConnection connection in board.connections)
            {
                connection.isUnlocked = false;
            }

        }
    }

    public DeductionBoardData GetCurrentBoard()
    {
        return allBoards[currentBoardIndex];

    }

    public void PlaceClue(ClueSlot slot, ClueData clue)
    {
        slot.placedClue = clue;
        Debug.Log("Placed " + clue.clueName + " in slot");
        CheckCompletion();
    }



    public void UnlockConnection(ClueConnection connection)
    {
        connection.isUnlocked = true;
        Debug.Log("Unlocked connection: " + connection.contextReason);
        CheckCompletion();

    }

    void CheckCompletion()
    {
        
        DeductionBoardData current = GetCurrentBoard();

        if (current.IsBoardComplete())
        {
            Debug.Log(current.boardName + " Complete!");

            foreach (DeductionBoardData board in current.boardsToUnlock)
            {
                board.isUnlocked = true;
                Debug.Log(board.boardName + " unlocked!");
            }
        }
    }


    public void SetCurrentBoard(DeductionBoardData board)
    {
        int index = allBoards.IndexOf(board);

        if (index >= 0)
        {
            currentBoardIndex = index;
        }
    }
}