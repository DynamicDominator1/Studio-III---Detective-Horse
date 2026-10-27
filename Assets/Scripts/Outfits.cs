using UnityEngine;

[RequireComponent(typeof(WorldStateManager))] // adds world state manager script to object
public class Outfit : Interactable
{
    public OutfitData outfitData; // outfit data assigned in inspector

    // when interacted with
    public override void Interact()
    {
        OutfitManager.Instance.AddOutfit(outfitData); // add to players outfit inventory
        NotificationUI.Instance.Show("Outfit found: " + outfitData.outfitName, outfitData.itemImage);
        WorldStateManager.Remove(gameObject);
    }
}