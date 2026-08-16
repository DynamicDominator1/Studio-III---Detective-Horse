using UnityEngine;


public class Door : Interactable
{
    public RoomEnum targetRoom;
    public SpawnPointEnum targetSpawnPoint; 

    public override void Interact()
    {
        RoomManager.Instance.GoToRoom(targetRoom, targetSpawnPoint);
    }
}