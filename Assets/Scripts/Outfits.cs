using UnityEngine;

public class Outfit : Interactable
{
    public OutfitData outfitData; 

    public override void Interact()
    {
        OutfitManager.Instance.AddOutfit(outfitData); 
        gameObject.SetActive(false); 
    }
}