using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance; 
    public List<ClueData> collectedClues = new List<ClueData>(); // clue data in inventory

    void Awake()
    {
        if (Instance == null) 
        {
            Instance = this; // set as singleton
        }
        else
        {
            Destroy(gameObject); // only 1 manager
        }
    }

    public void AddClue(ClueData clue)
    {
        collectedClues.Add(clue); // add clue to inventory
        Debug.Log("Picked up clue: " + clue.clueName);
    }
}