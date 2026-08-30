 using UnityEngine;

public class Clue : Interactable
{
    public ClueData clueData; // clue data

    public override void Interact()
    {
        InventoryManager.Instance.AddClue(clueData); // add clue data to inventory
        gameObject.SetActive(false); // hide picup
    }
}