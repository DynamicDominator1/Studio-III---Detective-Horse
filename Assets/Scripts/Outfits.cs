using UnityEngine;

public class Outfit : Interactable
{
    public OutfitData outfitData; // outfit data assigned in inspector

    // when interacted with
    public override void Interact()
    {
        OutfitManager.Instance.AddOutfit(outfitData); // add to players outfit inventory
        gameObject.SetActive(false); // hide object
    }
}