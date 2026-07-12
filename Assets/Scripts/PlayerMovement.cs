using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputAction moveAction; // input used to check if player wants to move
    public Vector2 moveInput;      // stores input value for movement
    public float speed = 5.0f;     // player movement speed
    public float rotationSpeed = 10f; // Horse turn speed
    public float gravity = -20f; // downward acceleration applied each frame
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
        controller.Move(moveDirection * speed * Time.deltaTime); // move the character

        // only rotate if there's actually movement input, otherwise LookRotation gets a zero vector and errors
        if (moveDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection); // rotation that faces movement direction
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); // smoothly rotate toward it
        }

        // gravity handling
        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f; // small constant downward force keeps the controller "grounded" and detecting ground properly each frame
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime; // accelerate downward over time, like real falling
        }

        Vector3 gravityMove = new Vector3(0f, verticalVelocity, 0f);
        controller.Move(gravityMove * Time.deltaTime); // apply the vertical movement separately
    }
}