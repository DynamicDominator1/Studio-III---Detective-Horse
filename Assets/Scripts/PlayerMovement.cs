using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputAction moveAction; 
    public Vector2 moveInput;      
    public float speed = 5.0f;     
    public float rotationSpeed = 720f; 
    public float gravity = -9.81f; 
    private float verticalVelocity; 

    private CharacterController controller;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Start()
    {
        moveAction.Enable(); 
    }

    void Update()
    {
        if (UIStateManager.Instance.isUIOpen) return;

        moveInput = moveAction.ReadValue<Vector2>(); 
        Vector3 moveDirection = new Vector3(moveInput.x, 0f, moveInput.y); 

        
        if (moveDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

       
        controller.Move(moveDirection * speed * Time.deltaTime);

        
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