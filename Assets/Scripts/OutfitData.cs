using UnityEngine;

[CreateAssetMenu(fileName = "New Outfit", menuName = "Detective Horse/Outfit")]
public class OutfitData : ScriptableObject
{
    public string outfitName; 
    public GameObject outfitModelPrefab; 
}