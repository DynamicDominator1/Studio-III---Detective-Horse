using UnityEngine;

[CreateAssetMenu(fileName = "New Clue", menuName = "Detective Horse/Clue")]
public class ClueData : ScriptableObject
{
    public string clueName;
    public string description;
    public Sprite itemImage; 
}