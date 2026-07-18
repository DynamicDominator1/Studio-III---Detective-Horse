using UnityEngine;

[CreateAssetMenu(fileName = "New Clue", menuName = "Detective Horse/Clue")]
public class ClueData : ScriptableObject
{
    public string clueName; // the clue's display name
    public string description; // what this clue tells the player
}