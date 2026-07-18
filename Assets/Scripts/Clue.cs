using UnityEngine;

public class Clue : Interactable
{
    public ClueData clueData; // drag the specific clue's data asset in here

    public override void Interact()
    {
        InventoryManager.Instance.AddClue(clueData); // hand the clue data off to the inventory
        gameObject.SetActive(false); // remove the clue from the world once picked up
    }
}