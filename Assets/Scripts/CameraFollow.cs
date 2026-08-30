using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //target player object
    public Transform target; // Hort

    //camera offset
    public Vector3 offset = new Vector3(0f, 10f, -7f);

    //camera speed offset
    public float smoothSpeed = 8f;

    // isometric camera angle
    public Vector3 rotationAngle = new Vector3(45f, 0f, 0f);

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset; // set cameras desiered position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime); // use linear interpolation to smooth camera movement
        transform.rotation = Quaternion.Euler(rotationAngle); // lock angel
    }
}