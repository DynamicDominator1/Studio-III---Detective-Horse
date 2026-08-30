using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string interactionName = ""; // interactable name
    public Material highlightMaterial; // material to highlight object if bit can be interacted with

    private Renderer objectRenderer; // object renderer used to swap mats
    private Material originalMaterial; // base objects mat

    void Awake()
    {
        objectRenderer = GetComponentInChildren<Renderer>(); // get renderer

        if (objectRenderer != null)
        {
            originalMaterial = objectRenderer.material; // remember base mat
        }
    }

    
    void Reset()
    {
        GetComponent<Collider>().isTrigger = true; // give interactabe trigger collider
        Rigidbody rb = gameObject.AddComponent<Rigidbody>(); // give interactble rigid body
        rb.isKinematic = true; // set true
        rb.useGravity = false; // set false

    }

    public virtual void Interact()
    {
        Debug.Log(interactionName + " interacted with");
    }

    // highlight mat on off
    public void SetHighlight(bool state)
    {
        if (objectRenderer == null) return;
        objectRenderer.material = state ? highlightMaterial : originalMaterial;
    }
}