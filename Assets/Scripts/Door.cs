using UnityEngine;


public class Door : Interactable
{
    public RoomEnum targetRoom;
    public SpawnPointEnum targetSpawnPoint; // spawn point within target room when using this door

    public override void Interact()
    {
        RoomManager.Instance.GoToRoom(targetRoom, targetSpawnPoint); // move player to target
    }
}