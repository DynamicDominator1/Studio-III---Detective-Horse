using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputAction moveAction; // move input (WASD)
    public Vector2 moveInput; // input value     
    public float speed = 5.0f;  
    public float rotationSpeed = 720f; 
    public float gravity = -9.81f; 
    private float verticalVelocity; 

    private CharacterController controller;

    void Awake()
    {
        controller = GetComponent<CharacterController>(); // character reference
    }

    void Start()
    {
        moveAction.Enable(); // enable moving inputs
    }

    void Update()
    {
        if (UIStateManager.Instance.isUIOpen) return; // cannot move while ui open

        moveInput = moveAction.ReadValue<Vector2>(); // current move input
        Vector3 moveDirection = new Vector3(moveInput.x, 0f, moveInput.y); // convert to vector 3 (3D)

        
        if (moveDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection); // rotate to move dir
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); // smooth rotation
        }

       
        controller.Move(moveDirection * speed * Time.deltaTime); // apply X and Z movement

        
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