using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class DedBoardUI : MonoBehaviour
{
    public Transform slotContainer; // clue slot visuals
    public GameObject slotPrefab;  
    public GameObject cluePickerPanel; 
    public Transform cluePickerGrid; // parent with buttons
    public GameObject cluePickerButtonPrefab; 
    public GameObject boardPanel;
    public InputAction toggleBoardAction; // open close input

    private ClueSlot slotBeingEdited; 
    private DedSlotUI slotUIBeingEdited;

    public Transform boardListContainer;
    public GameObject boardListButtonPrefab;

    public Sprite idleBoardSprite;
    public Sprite selectedBoardSprite;
    public Sprite completedBoardSprite;

    public GameObject stringLinePrefab;
    public Vector2 pinOffset = new Vector2(1.2f, 43.6f); // offset of visual indicator to look like a push pin

    public GameObject connectionTooltip;
    public TextMeshProUGUI tooltipText;

    void Start()
    {
        toggleBoardAction.Enable(); // enable input action
        boardPanel.SetActive(false); // hide panel at start

        cluePickerPanel.SetActive(false); // hide clue picker at start
    }

    void Update()
    {
        if (toggleBoardAction.WasPressedThisFrame()) // check if open close was inputted
        {
            ToggleBoard();
        }
    }


    void ToggleBoard()
    {
        bool isOpening = !boardPanel.activeSelf; // figures out if board is opening or closing

        if (isOpening)
        {
            UIStateManager.Instance.OpenPanel(boardPanel); // open panel
            ShowBoard(); // populate board
            RefreshBoardList(); // correctly set board switch list
        }
        else
        {
            UIStateManager.Instance.ClosePanel(boardPanel); // hide UI panel

        }
    }

    public void ShowBoard()
    {
        ConnectionLineUI.Init(connectionTooltip, tooltipText); // gives context box / text
        connectionTooltip.SetActive(false); // hide context

        
        // clear previous visuals
        foreach (Transform child in slotContainer)
        {
            Destroy(child.gameObject); // delete 
        }

        DeductionBoardData board = DeductionManager.Instance.GetCurrentBoard();

        // spawn slot for each clue
        foreach (ClueSlot slot in board.slots)
        {
            
            GameObject newSlotVisual = Instantiate(slotPrefab, slotContainer);
            newSlotVisual.GetComponent<RectTransform>().anchoredPosition = slot.uiPosition; // set pos

            DedSlotUI slotUI = newSlotVisual.GetComponent<DedSlotUI>();
            slotUI.Setup(slot, this); // link to clue data

        }

        // lines for each clue connection
        foreach (ClueConnection connection in board.connections)
        {
            if (connection.isUnlocked)
            {
                DrawConnection(board.slots[connection.slotAIndex], board.slots[connection.slotBIndex], connection.contextReason); // draws lines between A and B indexes
            }
        }
    }

    // Sets image between 2 points
    void DrawConnection(ClueSlot slotA, ClueSlot slotB, string reason)
    {
        GameObject line = Instantiate(stringLinePrefab, slotContainer);
        RectTransform lineRect = line.GetComponent<RectTransform>();

        Vector2 pointA = slotA.uiPosition + pinOffset; // slot A pos
        Vector2 pointB = slotB.uiPosition + pinOffset; // slot B pos

        Vector2 midpoint = (pointA + pointB) / 2f; // mid point
        float distance = Vector2.Distance(pointA, pointB); // get required size
        float angle = Mathf.Atan2(pointB.y - pointA.y, pointB.x - pointA.x) * Mathf.Rad2Deg; // get required rotation

        lineRect.anchoredPosition = midpoint; // place line at centre
        lineRect.sizeDelta = new Vector2(distance, lineRect.sizeDelta.y); // set size
        lineRect.rotation = Quaternion.Euler(0, 0, angle); // set rotation

        ConnectionLineUI lineUI = line.GetComponent<ConnectionLineUI>(); 
        lineUI.Setup(reason); // line gets contect text

    }

    public void OpenPicker(ClueSlot slot, DedSlotUI slotUI)
    {
        // finished boards cannot be edited
        if (DeductionManager.Instance.GetCurrentBoard().IsBoardComplete())
        {
            Debug.Log("This board is already complete.");
            return;
        }

        slotBeingEdited = slot;
        slotUIBeingEdited = slotUI;
        cluePickerPanel.SetActive(true);

        // destroy old buttons
        foreach (Transform child in cluePickerGrid)
        {
            Destroy(child.gameObject);
        }

        foreach (ClueData clue in InventoryManager.Instance.collectedClues) // each inventory clue gets a button
        {
            GameObject button = Instantiate(cluePickerButtonPrefab, cluePickerGrid); 
            button.GetComponentInChildren<TextMeshProUGUI>().text = clue.clueName;
            button.GetComponent<Button>().onClick.AddListener(() => SelectClue(clue)); // picks clue
        }
    }

    void SelectClue(ClueData clue)
    {
        DeductionManager.Instance.PlaceClue(slotBeingEdited, clue); // places clue in chosen spot
        slotUIBeingEdited.RefreshVisual(); // update visuals to be correct
        cluePickerPanel.SetActive(false); // hide panel

        RefreshBoardList(); 
    }

    public void RefreshBoardList()
    {
        foreach (Transform child in boardListContainer) // clear old buttons
        {
            Destroy(child.gameObject);
        }

        DeductionBoardData current = DeductionManager.Instance.GetCurrentBoard(); // each board gets a button

        foreach (DeductionBoardData board in DeductionManager.Instance.allBoards)
        {
            // locked boards arent visible
            if (!board.isUnlocked) continue;

            GameObject buttonObj = Instantiate(boardListButtonPrefab, boardListContainer);
            TextMeshProUGUI label = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
            Image background = buttonObj.GetComponent<Image>();


            // current board is highlighted
            if (board == current)
            {
                label.text = "Current";
                background.sprite = selectedBoardSprite;
            }
            else if (board.IsBoardComplete()) // check off completed boards
            {
                label.text = board.boardName; 
                background.sprite = completedBoardSprite;
            }
            else // show unfinished boards
            {
                label.text = board.boardName;
                background.sprite = idleBoardSprite;
            }

            buttonObj.GetComponent<Button>().onClick.AddListener(() => SwitchToBoard(board)); // buttons switch boards

        }
    }

    void SwitchToBoard(DeductionBoardData board)
    {
        DeductionManager.Instance.SetCurrentBoard(board); // update shown ded board

        ShowBoard();
    }

    public void OpenFromNav()
    {

        UIStateManager.Instance.OpenPanel(boardPanel);
        ShowBoard();
        RefreshBoardList();

    }
}