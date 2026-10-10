using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputAction moveAction; // move input (WASD)
    public InputAction runAction; // run input
    public Vector2 moveInput; // input value     
    public float speed = 5.0f; // normal walking speed
    public float runSpeed = 8.0f; // run speed
    public float rotationSpeed = 720f; // turn speed 
    public float turnHold = 0.06f; 
    public float gravity = -9.81f;
    private float verticalVelocity;

    private float turnTimer; 
    private Vector3 lastDiagonalDirection; 

    private CharacterController controller;

    void Awake()
    {
        controller = GetComponent<CharacterController>(); // character reference
    }

    void Start()
    {
        moveAction.Enable(); // enable moving inputs
        runAction.Enable(); // enable the run input so it can be read
    }

    void Update()
    {
        if (UIStateManager.Instance.isUIOpen) return; // cannot move while ui open

        moveInput = moveAction.ReadValue<Vector2>(); // current move input
        Vector3 moveDirection = new Vector3(moveInput.x, 0f, moveInput.y); // convert to vector 3 (3D)

        float currentSpeed = speed; // set walk speed

        if (runAction.IsPressed())
        {
            currentSpeed = runSpeed; // set run speed
        }

        bool isDiagonal = Mathf.Abs(moveInput.x) > 0.5f && Mathf.Abs(moveInput.y) > 0.5f; // true when both axes are held at once

        if (isDiagonal)
        {
            lastDiagonalDirection = moveDirection; // remember this diagonal
            turnTimer = turnHold; // refill the timer every frame a diagonal is held
        }

        if (turnTimer > 0f && !isDiagonal)
        {
            turnTimer -= Time.deltaTime; // tick the window down once the diagonal is no longer held
        }

        bool holdingFacing = turnTimer > 0f && !isDiagonal && moveDirection.sqrMagnitude > 0.01f; // true while we are inside the short window after a diagonal with one key still held

        if (moveDirection.sqrMagnitude > 0.01f && !holdingFacing) // only turn to the pressed key once the window has passed
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection); // turn to what is pressed right now
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); // smooth rotation
        }
        else if (moveDirection.sqrMagnitude <= 0.01f && turnTimer > 0f && lastDiagonalDirection.sqrMagnitude > 0.01f) // player stopped inside the window, so end on the diagonal
        {
            transform.rotation = Quaternion.LookRotation(lastDiagonalDirection); // jump straight to the diagonal
            turnTimer = 0f; // use the restore once so it can't repeat
        }

        controller.Move(moveDirection * currentSpeed * Time.deltaTime); // apply X and Z movement

        if (controller.isGrounded && verticalVelocity < 0) // check ground 
        {
            verticalVelocity = -2f; // keeps player on ground with small gravity force
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime; // gravity accumulation
        }

        Vector3 gravityMove = new Vector3(0f, verticalVelocity, 0f); // y movements from gravity
        controller.Move(gravityMove * Time.deltaTime); // apply y movement
    }
}