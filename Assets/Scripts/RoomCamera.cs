using UnityEngine;

public class RoomCamera : MonoBehaviour
{
    public CameraSettings settings; // this room's camera settings, edited in the Inspector

    void Start()
    {
        CameraFollow cam = FindFirstObjectByType<CameraFollow>(); // find the camera in the persistent scene

        if (cam != null) // only continue if the camera was actually found
        {
            cam.ApplySettings(settings); // hand the camera this room's settings, it jumps there instantly
        }
        else
        {
            Debug.LogWarning("RoomCamera could not find a CameraFollow in the loaded scenes"); // helps spot a missing camera while testing
        }
    }
}