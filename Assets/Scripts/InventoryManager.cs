using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance; // singleton reference, accessible from anywhere
    public List<ClueData> collectedClues = new List<ClueData>(); // all clues picked up so far

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // prevents duplicate InventoryManagers if this scene loads again
        }
    }

    public void AddClue(ClueData clue)
    {
        collectedClues.Add(clue);
        Debug.Log("Picked up clue: " + clue.clueName);
    }
}