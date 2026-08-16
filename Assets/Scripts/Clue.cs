 using UnityEngine;

public class Clue : Interactable
{
    public ClueData clueData; 

    public override void Interact()
    {
        InventoryManager.Instance.AddClue(clueData); 
        gameObject.SetActive(false); // hide picup
    }
}