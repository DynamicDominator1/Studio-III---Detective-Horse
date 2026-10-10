using UnityEngine;

[CreateAssetMenu(fileName = "New Outfit", menuName = "Detective Horse/Outfit")]
public class OutfitData : ScriptableObject
{
    public string outfitName; // displayed name
    public Sprite itemImage;
    public GameObject outfitModelPrefab; // player prefab to spawn when outfit equipped
}