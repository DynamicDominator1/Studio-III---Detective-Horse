using UnityEngine;

[RequireComponent(typeof(WorldStateManager))] // adds world state manager script to object
public class Clue : Interactable
{
    public ClueData clueData; 

    public override void Interact()
    {
        InventoryManager.Instance.AddClue(clueData); 
        NotificationUI.Instance.Show("Clue found: " + clueData.clueName, clueData.itemImage);
        WorldStateManager.Remove(gameObject);
    }
}