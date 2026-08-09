using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class OutfitManager : MonoBehaviour
{
    public static OutfitManager Instance;
    public OutfitData baseHorseFit; 
    public List<OutfitData> ownedOutfits = new List<OutfitData>(); 
    public OutfitData currentOutfit; 

    public InputAction switchOutfitAction; 
    public Transform outfitAttachPoint; 

    private GameObject currentOutfitInstance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        switchOutfitAction.Enable();

        
        foreach (Transform child in outfitAttachPoint)
        {
            Destroy(child.gameObject);
        }

        EquipOutfit(baseHorseFit); 
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
        ownedOutfits.Add(outfit);
        Debug.Log("Picked up outfit: " + outfit.outfitName);
    }

    public void EquipOutfit(OutfitData outfit)
    {
        if (outfit != baseHorseFit && !ownedOutfits.Contains(outfit)) return; 

        if (currentOutfitInstance != null)
        {
            Destroy(currentOutfitInstance);
        }

        currentOutfit = outfit;

        if (outfit.outfitModelPrefab != null)
        {
            currentOutfitInstance = Instantiate(outfit.outfitModelPrefab, outfitAttachPoint.position, outfitAttachPoint.rotation, outfitAttachPoint);
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