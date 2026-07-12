using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;        // the player object to follow
    public Vector3 offset = new Vector3(0f, 10f, -7f); // distance/angle from player
    public float smoothSpeed = 8f;  // how quickly camera catches up to target
    public Vector3 rotationAngle = new Vector3(45f, 0f, 0f); // camera tilt, editable in inspector

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Euler(rotationAngle);
    }
}