using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputAction moveAction; // input used to check if player wants to move
    public Vector2 moveInput;      // stores input value for movement
    public float speed = 5.0f;     // player movement speed
    public float rotationSpeed = 720f; // degrees the horse can turn per second
    public float gravity = -9.81f; // downward acceleration applied each frame
    private float verticalVelocity; // tracks current fall speed

    private CharacterController controller;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Start()
    {
        moveAction.Enable(); // enables game to listen for input from move action
    }

    void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>(); // reads current move input value
        Vector3 moveDirection = new Vector3(moveInput.x, 0f, moveInput.y); // turn 2D input into 3D direction

        // rotate first, before any movement happens this frame
        if (moveDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // movement happens after rotation is applied
        controller.Move(moveDirection * speed * Time.deltaTime);

        // gravity handling
        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        Vector3 gravityMove = new Vector3(0f, verticalVelocity, 0f);
        controller.Move(gravityMove * Time.deltaTime);
    }
}