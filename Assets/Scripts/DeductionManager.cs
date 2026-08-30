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
            Instance = this; // set ded manager
        }
        else
        {
            Destroy(gameObject); // ensure only 1 ded manager
        }

#if UNITY_EDITOR
        ResetAllBoardsForTesting(); // resets boards within editor for testing
#endif

    }

    // resets references in unity for ded board before game compiles
    void ResetAllBoardsForTesting()
    {
        for (int i = 0; i < allBoards.Count; i++)
        {
            DeductionBoardData board = allBoards[i];
            board.isUnlocked = (i == 0); // game start with ded board 1 unlocked

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
        return allBoards[currentBoardIndex]; // current board

    }

    public void PlaceClue(ClueSlot slot, ClueData clue)
    {
        slot.placedClue = clue; // assign clue to slot 
        Debug.Log("Placed " + clue.clueName + " in slot");
        CheckCompletion(); // check if board completed
    }



    public void UnlockConnection(ClueConnection connection)
    {
        connection.isUnlocked = true; // assign connection
        Debug.Log("Unlocked connection: " + connection.contextReason);
        CheckCompletion();

    }

    void CheckCompletion()
    {
        
        DeductionBoardData current = GetCurrentBoard();

        // ded board completion check
        if (current.IsBoardComplete())
        {
            Debug.Log(current.boardName + " Complete!");

            // next board(s) unlocked
            foreach (DeductionBoardData board in current.boardsToUnlock)
            {
                board.isUnlocked = true;
                Debug.Log(board.boardName + " unlocked!");
            }
        }
    }


    public void SetCurrentBoard(DeductionBoardData board)
    {
        int index = allBoards.IndexOf(board); // find board index

        if (index >= 0) // if board exists
        {
            currentBoardIndex = index; // change boards
        }
    }
}