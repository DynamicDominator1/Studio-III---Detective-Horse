using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string interactionName = "Clue"; // what this object is, shown in prompts/logs later
    public bool playerInRange = false;       // true when the player is standing in this object's trigger

    void Reset()
    {
        // runs automatically when the component is first added - reminds you to set up the collider correctly
        GetComponent<Collider>().isTrigger = true;

        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    public virtual void Interact()
    {
        // placeholder for now - each object type (clue, NPC, outfit) can override this later with real behaviour
        Debug.Log(interactionName + " interacted with");
    }
}