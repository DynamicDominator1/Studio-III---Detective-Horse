using UnityEngine;

[CreateAssetMenu(fileName = "New Outfit", menuName = "Detective Horse/Outfit")]
public class OutfitData : ScriptableObject
{
    public string outfitName; // display name of the outfit
    public GameObject outfitModelPrefab; // the visual model to show when this outfit is equipped
}