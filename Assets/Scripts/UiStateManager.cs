using UnityEngine;
using UnityEngine.InputSystem;

public class UIStateManager : MonoBehaviour
{
    public static UIStateManager Instance;
    public bool isUIOpen = false;
    public InputAction escapeAction;

    public AudioClip openSound;
    public AudioClip closeSound;
    public GameObject[] silentPanels; // UI panels to play no sound


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

    
    bool IsSilent(GameObject panel)
    {
        foreach (GameObject silentPanel in silentPanels) // search silent panel list for current UI
        {
            if (silentPanel == panel) return true; // set panel silent
        }

        return false; // play panels sound
    }

    public void OpenPanel(GameObject panel, bool allowEscapeClose = true)
    {
        bool wasAlreadyOpen = panel.activeSelf;

        if (currentPanel != null && currentPanel != panel) // check if other UI panel open
        {
            currentPanel.SetActive(false); // close that
        }

        panel.SetActive(true); // open new panel
        currentPanel = panel; // new panel is current panel
        currentPanelAllowsEscape = allowEscapeClose; // some panels cannot be esc from
        isUIOpen = true; // set ui as open

        if (!wasAlreadyOpen && !IsSilent(panel) && AudioManager.Instance != null) // play audio when panel opens. empty = no sound. silent panels skip
        {
            AudioManager.Instance.PlaySFX(openSound);
        }
    }

    public void ClosePanel(GameObject panel)
    {
        bool wasOpen = panel.activeSelf;


        panel.SetActive(false); // hide panel

        // clear current panel
        if (currentPanel == panel)
        {
            currentPanel = null;
        }
        isUIOpen = false; // set ui as closed

        if (wasOpen && !IsSilent(panel) && AudioManager.Instance != null) 
        {
            AudioManager.Instance.PlaySFX(closeSound);
        }
    }

    public void CloseCurrentPanel()
    {
        if (currentPanel != null) // close if UI exists
        {
            ClosePanel(currentPanel);
        }
    }
}