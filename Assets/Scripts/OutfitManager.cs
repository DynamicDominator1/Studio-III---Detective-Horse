using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class OutfitManager : MonoBehaviour
{
    public static OutfitManager Instance;
    public OutfitData baseHorseFit; // drag the "No Outfit" / bare horse asset in here
    public List<OutfitData> ownedOutfits = new List<OutfitData>(); // every outfit the player has picked up (does NOT include noOutfitData)
    public OutfitData currentOutfit; // whichever outfit is currently equipped

    public InputAction switchOutfitAction; // temporary key to cycle through owned outfits, for testing before UI exists
    public Transform outfitAttachPoint; // where the outfit model should be instantiated on the player

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

        // destroy whatever's sitting under the attach point in the editor (e.g. your placeholder Horse Model), so we start fresh at runtime
        foreach (Transform child in outfitAttachPoint)
        {
            Destroy(child.gameObject);
        }

        EquipOutfit(baseHorseFit); // start the game with no outfit equipped
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
        if (outfit != baseHorseFit && !ownedOutfits.Contains(outfit)) return; // safety check - can't equip an outfit you don't own (noOutfitData is always allowed)

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

    // Cycles through: No Outfit -> owned outfit 1 -> owned outfit 2 -> back to No Outfit
    void CycleOutfit()
    {
        List<OutfitData> allStates = new List<OutfitData> { baseHorseFit };
        allStates.AddRange(ownedOutfits);

        int currentIndex = allStates.IndexOf(currentOutfit);
        int nextIndex = (currentIndex + 1) % allStates.Count;

        EquipOutfit(allStates[nextIndex]);
    }
}