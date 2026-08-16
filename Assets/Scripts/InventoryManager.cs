using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<ClueData> collectedClues = new List<ClueData>();
    public int selectedClueIndex = 0; // which clue in the list is currently "selected"

    public InputAction cycleClueAction; 

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
    }

    void Start()
    {
        cycleClueAction.Enable();
    }

    void Update()
    {
        if (cycleClueAction.WasPressedThisFrame())
        {
            CycleSelectedClue();
        }
    }

    public void AddClue(ClueData clue)
    {
        collectedClues.Add(clue);
        Debug.Log("Picked up clue: " + clue.clueName);
    }

    void CycleSelectedClue()
    {
        if (collectedClues.Count == 0) return;

        selectedClueIndex = (selectedClueIndex + 1) % collectedClues.Count;
        Debug.Log("Selected clue: " + collectedClues[selectedClueIndex].clueName);
    }
}