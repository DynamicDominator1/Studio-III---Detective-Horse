using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class OutfitManager : MonoBehaviour
{
    public static OutfitManager Instance;
    public OutfitData baseHorseFit; // default outfit
    public List<OutfitData> ownedOutfits = new List<OutfitData>();  // outfit inventory
    public OutfitData currentOutfit;

    public InputAction switchOutfitAction; // input to cycle through outfits (for testing)
    public Transform outfitAttachPoint; // where to spawn each outfit player model

    private GameObject currentOutfitInstance; // currently spawned outfit model

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // set singleton outfit manager
        }
        else
        {
            Destroy(gameObject); // only 1
        }
    }

    void Start()
    {
        switchOutfitAction.Enable(); // enable cycle  input 

        
        foreach (Transform child in outfitAttachPoint) // clear model
        {
            Destroy(child.gameObject);
        }

        EquipOutfit(baseHorseFit); // set base outfit
    }

    void Update()
    {
        if (switchOutfitAction.WasPressedThisFrame())
        {
            CycleOutfit();
        }
    }

    public void AddOutfit(OutfitData outfit)
    {
        ownedOutfits.Add(outfit); // add fit to inventory
        Debug.Log("Picked up outfit: " + outfit.outfitName);
    }

    public void EquipOutfit(OutfitData outfit)
    {
        if (outfit != baseHorseFit && !ownedOutfits.Contains(outfit)) return; // unowned outfits cannot be equipped

        if (currentOutfitInstance != null)
        {
            Destroy(currentOutfitInstance); // remove current outfit
        }

        currentOutfit = outfit; // update current outfit

        if (outfit.outfitModelPrefab != null)
        {
            currentOutfitInstance = Instantiate(outfit.outfitModelPrefab, outfitAttachPoint.position, outfitAttachPoint.rotation, outfitAttachPoint); // spawn outfit model at attach point
        }

        Debug.Log("Now wearing: " + currentOutfit.outfitName);
    }

    
    void CycleOutfit()
    {
        List<OutfitData> allStates = new List<OutfitData> { baseHorseFit };
        allStates.AddRange(ownedOutfits);

        int currentIndex = allStates.IndexOf(currentOutfit);
        int nextIndex = (currentIndex + 1) % allStates.Count;

        EquipOutfit(allStates[nextIndex]);
    }
}