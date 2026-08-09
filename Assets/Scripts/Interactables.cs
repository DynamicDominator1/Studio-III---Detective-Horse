using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string interactionName = "";
    public bool playerInRange = false;
    public Material highlightMaterial; 

    private Renderer objectRenderer;
    private Material originalMaterial;

    void Awake()
    {
        objectRenderer = GetComponentInChildren<Renderer>();
        if (objectRenderer != null)
        {
            originalMaterial = objectRenderer.material;
        }
    }

    void Reset()
    {
        GetComponent<Collider>().isTrigger = true;
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    public virtual void Interact()
    {
        Debug.Log(interactionName + " interacted with");
    }

    public void SetHighlight(bool state)
    {
        if (objectRenderer == null) return;

        objectRenderer.material = state ? highlightMaterial : originalMaterial;
    }
}