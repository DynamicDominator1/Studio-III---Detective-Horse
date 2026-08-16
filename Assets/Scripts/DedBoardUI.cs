using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class DedBoardUI : MonoBehaviour
{
    public Transform slotContainer;
    public GameObject slotPrefab;
    public GameObject cluePickerPanel;
    public Transform cluePickerGrid;
    public GameObject cluePickerButtonPrefab;
    public GameObject boardPanel;
    public InputAction toggleBoardAction;

    private ClueSlot slotBeingEdited;
    private DedSlotUI slotUIBeingEdited;

    public Transform boardListContainer;
    public GameObject boardListButtonPrefab;

    public Sprite idleBoardSprite;
    public Sprite selectedBoardSprite;
    public Sprite completedBoardSprite;

    public GameObject stringLinePrefab;
    public Vector2 pinOffset = new Vector2(1.2f, 43.6f);

    public GameObject connectionTooltip;
    public TextMeshProUGUI tooltipText;

    void Start()
    {
        toggleBoardAction.Enable();
        boardPanel.SetActive(false);

        cluePickerPanel.SetActive(false);
    }

    void Update()
    {
        if (toggleBoardAction.WasPressedThisFrame())
        {
            ToggleBoard();
        }
    }


    void ToggleBoard()
    {
        bool isOpening = !boardPanel.activeSelf;

        if (isOpening)
        {
            UIStateManager.Instance.OpenPanel(boardPanel);
            ShowBoard();
            RefreshBoardList();
        }
        else
        {
            UIStateManager.Instance.ClosePanel(boardPanel);

        }
    }

    public void ShowBoard()
    {
        ConnectionLineUI.Init(connectionTooltip, tooltipText);
        connectionTooltip.SetActive(false);

        
        foreach (Transform child in slotContainer)
        {
            Destroy(child.gameObject);
        }

        DeductionBoardData board = DeductionManager.Instance.GetCurrentBoard();

        foreach (ClueSlot slot in board.slots)
        {
            
            GameObject newSlotVisual = Instantiate(slotPrefab, slotContainer);
            newSlotVisual.GetComponent<RectTransform>().anchoredPosition = slot.uiPosition;

            DedSlotUI slotUI = newSlotVisual.GetComponent<DedSlotUI>();
            slotUI.Setup(slot, this);

        }

        foreach (ClueConnection connection in board.connections)
        {
            if (connection.isUnlocked)
            {
                DrawConnection(board.slots[connection.slotAIndex], board.slots[connection.slotBIndex], connection.contextReason);
            }
        }
    }

    // Stretches image between 2 points to act as string
    void DrawConnection(ClueSlot slotA, ClueSlot slotB, string reason)
    {
        GameObject line = Instantiate(stringLinePrefab, slotContainer);
        RectTransform lineRect = line.GetComponent<RectTransform>();

        Vector2 pointA = slotA.uiPosition + pinOffset;
        Vector2 pointB = slotB.uiPosition + pinOffset;

        Vector2 midpoint = (pointA + pointB) / 2f;
        float distance = Vector2.Distance(pointA, pointB);
        float angle = Mathf.Atan2(pointB.y - pointA.y, pointB.x - pointA.x) * Mathf.Rad2Deg;

        lineRect.anchoredPosition = midpoint;
        lineRect.sizeDelta = new Vector2(distance, lineRect.sizeDelta.y);
        lineRect.rotation = Quaternion.Euler(0, 0, angle);

        ConnectionLineUI lineUI = line.GetComponent<ConnectionLineUI>();
        lineUI.Setup(reason);

    }

    public void OpenPicker(ClueSlot slot, DedSlotUI slotUI)
    {
        if (DeductionManager.Instance.GetCurrentBoard().IsBoardComplete())
        {
            Debug.Log("This board is already complete.");
            return;
        }

        slotBeingEdited = slot;
        slotUIBeingEdited = slotUI;
        cluePickerPanel.SetActive(true);

        foreach (Transform child in cluePickerGrid)
        {
            Destroy(child.gameObject);
        }

        foreach (ClueData clue in InventoryManager.Instance.collectedClues)
        {
            GameObject button = Instantiate(cluePickerButtonPrefab, cluePickerGrid);
            button.GetComponentInChildren<TextMeshProUGUI>().text = clue.clueName;
            button.GetComponent<Button>().onClick.AddListener(() => SelectClue(clue));
        }
    }

    void SelectClue(ClueData clue)
    {
        DeductionManager.Instance.PlaceClue(slotBeingEdited, clue);
        slotUIBeingEdited.RefreshVisual();
        cluePickerPanel.SetActive(false);

        RefreshBoardList();
    }

    public void RefreshBoardList()
    {
        foreach (Transform child in boardListContainer)
        {
            Destroy(child.gameObject);
        }

        DeductionBoardData current = DeductionManager.Instance.GetCurrentBoard();

        foreach (DeductionBoardData board in DeductionManager.Instance.allBoards)
        {
            // locked boards just don't show up at all rather than being greyed out
            if (!board.isUnlocked) continue;

            GameObject buttonObj = Instantiate(boardListButtonPrefab, boardListContainer);
            TextMeshProUGUI label = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
            Image background = buttonObj.GetComponent<Image>();


            if (board == current)
            {
                label.text = "Current";
                background.sprite = selectedBoardSprite;
            }
            else if (board.IsBoardComplete())
            {
                label.text = board.boardName;
                background.sprite = completedBoardSprite;
            }
            else
            {
                label.text = board.boardName;
                background.sprite = idleBoardSprite;
            }

            buttonObj.GetComponent<Button>().onClick.AddListener(() => SwitchToBoard(board));

        }
    }

    void SwitchToBoard(DeductionBoardData board)
    {
        DeductionManager.Instance.SetCurrentBoard(board);

        ShowBoard();
    }

    public void OpenFromNav()
    {

        UIStateManager.Instance.OpenPanel(boardPanel);
        ShowBoard();
        RefreshBoardList();

    }
}