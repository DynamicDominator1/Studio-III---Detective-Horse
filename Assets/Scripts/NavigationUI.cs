using UnityEngine;
using UnityEngine.UI;

public class NavigationUI : MonoBehaviour
{
    public Button inventoryButton;
    public Button boardsButton;
    public Button outfitsButton;
    public Button settingsButton;
    public Button exitButton;

    public InventoryUI inventoryUI;
    public OutfitUI outfitUI;
    public DedBoardUI dedBoardUI;
    

    void Start()
    {
        inventoryButton.onClick.AddListener(() => inventoryUI.OpenFromNav());
        boardsButton.onClick.AddListener(() => dedBoardUI.OpenFromNav());
        outfitsButton.onClick.AddListener(() => outfitUI.OpenFromNav());
        exitButton.onClick.AddListener(() => UIStateManager.Instance.CloseCurrentPanel());
    }
}