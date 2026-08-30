using UnityEngine;

[CreateAssetMenu(fileName = "New Clue", menuName = "Detective Horse/Clue")]
public class ClueData : ScriptableObject
{
    public string clueName; // clue name
    public string description; // clue desc
    public Sprite itemImage; // clue image
}