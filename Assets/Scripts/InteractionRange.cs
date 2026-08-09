using UnityEngine;

public class InteractionRange : MonoBehaviour
{
    public Interactable currentInteractable;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by: " + other.gameObject.name);

        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable != null)
        {
            currentInteractable = interactable;
            Debug.Log("Found Interactable: " + interactable.interactionName);
            interactable.SetHighlight(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable != null && interactable == currentInteractable)
        {
            interactable.SetHighlight(false);
            currentInteractable = null;
            Debug.Log("Left range of: " + interactable.interactionName);
        }
    }
}