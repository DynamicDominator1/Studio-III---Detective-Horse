using UnityEngine;
using UnityEngine.InputSystem;

public class HorseInteraction : MonoBehaviour
{
    public InputAction interactAction; 
    public Interactable currentInteractable; // the Interactable the player is currently standing near

    void Start()
    {
        interactAction.Enable(); // enables game to listen for input from interact action
    }

    void Update()
    {
        if (interactAction.WasPressedThisFrame() && currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable != null)
        {
            currentInteractable = interactable;
            currentInteractable.playerInRange = true;
            Debug.Log("In range of: " + interactable.interactionName);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable != null && interactable == currentInteractable)
        {
            currentInteractable.playerInRange = false;
            currentInteractable = null;
            Debug.Log("Out of range of: " + interactable.interactionName);
        }
    }
}