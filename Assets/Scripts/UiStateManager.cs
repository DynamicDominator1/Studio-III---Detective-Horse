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
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        escapeAction.Enable();
    }

    void Update()
    {
        if (escapeAction.WasPressedThisFrame() && isUIOpen && currentPanelAllowsEscape)
        {
            ClosePanel(currentPanel);
        }
    }

    public void OpenPanel(GameObject panel, bool allowEscapeClose = true)
    {
        if (currentPanel != null && currentPanel != panel)
        {
            currentPanel.SetActive(false);
        }

        panel.SetActive(true);
        currentPanel = panel;
        currentPanelAllowsEscape = allowEscapeClose;
        isUIOpen = true;
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
        if (currentPanel == panel)
        {
            currentPanel = null;
        }
        isUIOpen = false;
    }

    public void CloseCurrentPanel()
    {
        if (currentPanel != null)
        {
            ClosePanel(currentPanel);
        }
    }
}