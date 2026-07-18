using UnityEngine;
using UnityEngine.InputSystem;

public class HorseInteraction : MonoBehaviour
{
    public InputAction interactAction; // input action (E key) used to check if player wants to interact
    public InteractionRange interactionRange; // drag the InteractionRange child object in here

    void Start()
    {
        interactAction.Enable();
    }

    void Update()
    {
        if (interactAction.WasPressedThisFrame())
        {
            Debug.Log("E pressed. Current interactable: " + interactionRange.currentInteractable); // confirms key press is detected and shows what's currently in range

            if (interactionRange.currentInteractable != null)
            {
                interactionRange.currentInteractable.Interact();
            }
        }
    }
}