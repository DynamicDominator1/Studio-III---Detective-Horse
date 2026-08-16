using UnityEngine;
using UnityEngine.InputSystem;

public class HorseInteraction : MonoBehaviour
{
    public InputAction interactAction; 
    public InteractionRange interactionRange; 

    void Start()
    {
        interactAction.Enable();

    }

    void Update()
    {
        if (UIStateManager.Instance.isUIOpen) return;

        if (interactAction.WasPressedThisFrame())
        {
            // Debug.Log("E pressed. Current interactable: " + interactionRange.currentInteractable); 

            if (interactionRange.currentInteractable != null)
            {
                interactionRange.currentInteractable.Interact();

            }
        }
    }
}