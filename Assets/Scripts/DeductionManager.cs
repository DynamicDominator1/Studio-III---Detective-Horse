using UnityEngine;
using System.Collections.Generic;

public class DeductionManager : MonoBehaviour
{
    public static DeductionManager Instance;
    public List<DeductionBoardData> allBoards; // every board in the game, in order
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

    void ResetAllBoardsForTesting()
    {
        for (int i = 0; i < allBoards.Count; i++)
        {
            DeductionBoardData board = allBoards[i];

            board.isUnlocked = (i == 0); // only the first board starts unlocked, every other board re-locks

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

    public void NextBoard()
    {
        if (currentBoardIndex < allBoards.Count - 1 && allBoards[currentBoardIndex + 1].isUnlocked)
        {
            currentBoardIndex++;
        }
    }

    public void PreviousBoard()
    {
        if (currentBoardIndex > 0)
        {
            currentBoardIndex--;
        }
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

    public void ResetCurrentBoardForTesting()
    {
        DeductionBoardData board = GetCurrentBoard();
        foreach (ClueSlot slot in board.slots)
        {
            slot.placedClue = null;
        }
        foreach (ClueConnection connection in board.connections)
        {
            connection.isUnlocked = false;
        }
        Debug.Log("Board reset for testing.");
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