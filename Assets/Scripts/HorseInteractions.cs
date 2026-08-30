using UnityEngine;
using UnityEngine.InputSystem;

public class HorseInteraction : MonoBehaviour
{
    public InputAction interactAction; // interaction input 
    public InteractionRange interactionRange; // detects interactables

    void Start()
    {
        interactAction.Enable(); // enable action

    }

    void Update()
    {
        if (UIStateManager.Instance.isUIOpen) return; // no interactions while in ui


        if (interactAction.WasPressedThisFrame()) // check for interaction input
        {
            Debug.Log("E pressed. Current interactable: " + interactionRange.currentInteractable); 

            if (interactionRange.currentInteractable != null)
            {
                interactionRange.currentInteractable.Interact(); // trigger objects interaction

            }
        }
    }
}