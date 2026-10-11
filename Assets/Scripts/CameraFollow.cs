using UnityEngine;

[System.Serializable]
public class CameraSettings
{
    public bool follow = true; 
    public Vector3 offset = new Vector3(0f, 10f, -7f); 
    public Vector3 fixedPosition; 
    public Vector3 rotationAngle = new Vector3(45f, 0f, 0f); 
}

public class CameraFollow : MonoBehaviour
{
    //target player object
    public Transform target; // Hort

    public bool follow = true; // true = follow , false = stay 

    //camera offset
    public Vector3 offset = new Vector3(0f, 10f, -7f);

    public Vector3 fixedPosition; // stationary camera location

    //camera speed offset
    public float smoothSpeed = 8f;

    // isometric camera angle
    public Vector3 rotationAngle = new Vector3(45f, 0f, 0f);

    // NEW: called by a room to hand the camera that room's settings, jumps there instantly (the screen is faded out)
    public void ApplySettings(CameraSettings settings)
    {
        follow = settings.follow; // copy follow or stationary mode
        offset = settings.offset; // copy the follow offset
        fixedPosition = settings.fixedPosition; // copy the stationary position
        rotationAngle = settings.rotationAngle; // copy the camera angle

        transform.position = GetDesiredPosition(); 
        transform.rotation = Quaternion.Euler(rotationAngle); 
    }

    
    Vector3 GetDesiredPosition()
    {
        if (follow) // follow rooms track Hort
        {
            return target.position + offset; // set cameras desiered position
        }

        return fixedPosition; // stationary rooms stay at the fixed position
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = GetDesiredPosition(); 
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime); // use linear interpolation to smooth camera movement
        transform.rotation = Quaternion.Euler(rotationAngle); // lock angel
    }
}