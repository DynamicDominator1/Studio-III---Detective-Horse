using UnityEngine;

public class Clue : Interactable
{
    public ClueData clueData; 

    public override void Interact()
    {
        InventoryManager.Instance.AddClue(clueData); 
        NotificationUI.Instance.Show("Clue found: " + clueData.clueName, clueData.itemImage); 
        gameObject.SetActive(false); 
    }
}