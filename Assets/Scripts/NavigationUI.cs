using UnityEngine;
using UnityEngine.UI;

public class NavigationUI : MonoBehaviour
{
    public Button inventoryButton; // sets inventory
    public Button boardsButton; // sets ded board
    public Button outfitsButton; // sets Outfit inventory
    public Button settingsButton; // NOT YET IMPLIMENTED
    public Button exitButton; // sets close

    // script references
    public InventoryUI inventoryUI;
    public OutfitUI outfitUI;
    public DedBoardUI dedBoardUI;
    

    void Start()
    {
        inventoryButton.onClick.AddListener(() => inventoryUI.OpenFromNav()); // opens inventory on click
        boardsButton.onClick.AddListener(() => dedBoardUI.OpenFromNav()); // opens ded board on click
        outfitsButton.onClick.AddListener(() => outfitUI.OpenFromNav()); // opens outfit inventory on click
        exitButton.onClick.AddListener(() => UIStateManager.Instance.CloseCurrentPanel()); // close ui on click
    }
}