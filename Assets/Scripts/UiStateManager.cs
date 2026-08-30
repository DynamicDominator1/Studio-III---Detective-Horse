using UnityEngine;
using UnityEngine.InputSystem;

public class UIStateManager : MonoBehaviour
{
    public static UIStateManager Instance;
    public bool isUIOpen = false;
    public InputAction escapeAction; 

    private GameObject currentPanel;
    private bool currentPanelAllowsEscape;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // set singleton
        }
        else
        {
            Destroy(gameObject); // only 1 ui manager
        }
    }

    void Start()
    {
        escapeAction.Enable(); // enables input of esc input
    }

    // allows esc to exit UI
    void Update()
    {
        if (escapeAction.WasPressedThisFrame() && isUIOpen && currentPanelAllowsEscape)
        {
            ClosePanel(currentPanel);
        }
    }

    public void OpenPanel(GameObject panel, bool allowEscapeClose = true)
    {
        if (currentPanel != null && currentPanel != panel) // check if other UI panel open
        {
            currentPanel.SetActive(false); // close that
        }

        panel.SetActive(true); // open new panel
        currentPanel = panel; // new panel is current panel
        currentPanelAllowsEscape = allowEscapeClose; // some panels (dialouge) cannot be esc from
        isUIOpen = true; // set ui as open
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false); // hide panel
       
        // clear current panel
        if (currentPanel == panel) 
        {
            currentPanel = null;
        }
        isUIOpen = false; // set ui as closed
    }

    public void CloseCurrentPanel()
    {
        if (currentPanel != null) // close if UI exists
        {
            ClosePanel(currentPanel);
        }
    }
}