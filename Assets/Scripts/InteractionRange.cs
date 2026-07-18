using UnityEngine;

public class InteractionRange : MonoBehaviour
{
    public Interactable currentInteractable; // whichever Interactable this collider is currently touching

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by: " + other.gameObject.name); // confirms the trigger is firing at all, and what it touched

        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable != null)
        {
            currentInteractable = interactable;
            Debug.Log("Found Interactable: " + interactable.interactionName);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable != null && interactable == currentInteractable)
        {
            currentInteractable = null;
            Debug.Log("Left range of: " + interactable.interactionName);
        }
    }
}