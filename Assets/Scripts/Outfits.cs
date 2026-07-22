using UnityEngine;

public class Outfit : Interactable
{
    public OutfitData outfitData; // drag the specific outfit's data asset in here

    public override void Interact()
    {
        OutfitManager.Instance.AddOutfit(outfitData); // hand the outfit off to the outfit manager
        gameObject.SetActive(false); // remove the pickup from the world once collected
    }
}