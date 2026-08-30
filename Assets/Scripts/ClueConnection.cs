using UnityEngine;

[System.Serializable]
public class ClueConnection
{
    // connection points
    public int slotAIndex; 
    public int slotBIndex; 

    public string contextReason; // conection description displayed to player
    public bool isUnlocked; // unlocks connection


}