using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Hort
    public Vector3 offset = new Vector3(0f, 10f, -7f);
    public float smoothSpeed = 8f;

    // isometric
    public Vector3 rotationAngle = new Vector3(45f, 0f, 0f);

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(rotationAngle);
    }
}