using UnityEngine;

public class RoomCamera : MonoBehaviour
{
    public CameraSettings settings; 

    void Start()
    {
        CameraFollow cam = FindFirstObjectByType<CameraFollow>(); 

        if (cam != null) 
        {
            cam.ApplySettings(settings); 
        }
        else
        {
            Debug.LogWarning("could not find camera"); 
        }
    }
}