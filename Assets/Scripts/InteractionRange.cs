using UnityEngine;

public class InteractionRange : MonoBehaviour
{
    public Interactable currentInteractable; // interactable object in range

    // when another collider enters trigger zone
    void OnTriggerEnter(Collider other) 
    {
        Debug.Log("Trigger entered by: " + other.gameObject.name);

        Interactable interactable = other.GetComponent<Interactable>(); // check if object is interactabe
        if (interactable != null)
        {
            currentInteractable = interactable; // set current interactable
            Debug.Log("Found Interactable: " + interactable.interactionName);
            interactable.SetHighlight(true); // set highlight mat
        }
    }

    // when another collider exits trigger zone
    void OnTriggerExit(Collider other)
    {
        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable != null && interactable == currentInteractable)
        {
            interactable.SetHighlight(false); // set base mat
            currentInteractable = null; // clear interactable reference
            Debug.Log("Left range of: " + interactable.interactionName);
        }
    }
}