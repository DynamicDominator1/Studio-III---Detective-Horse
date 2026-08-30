using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI Instance;

    public GameObject dialoguePanel;
    public TextMeshProUGUI npcLineText;
    public Transform optionsContainer;
    public GameObject optionButtonPrefab;

    private NPC currentNPC; // npc being spoken to

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); //Destroy duplicates
        }
    }

    void Start()
    {
        dialoguePanel.SetActive(false); // panel hidden at start 
    }

    public void OpenDialogue(NPC npc, string line, List<Dialogue> options)
    {
        currentNPC = npc; 
        UIStateManager.Instance.OpenPanel(dialoguePanel); // show UI
        UpdateDialogue(line, options);  // populate with correct text
    }


    public void UpdateDialogue(string line, List<Dialogue> options)
    {
        npcLineText.text = line; // set npc current line

        // clear old player responses
        foreach (Transform child in optionsContainer) 
        {
            Destroy(child.gameObject); // delete
        }


        // each response gets a button
        foreach (Dialogue option in options)
        {
            bool isUnlocked = option.requiredClue == null || InventoryManager.Instance.collectedClues.Contains(option.requiredClue); // does the player have required unlocks for this

            GameObject buttonObj = Instantiate(optionButtonPrefab, optionsContainer);
            TextMeshProUGUI label = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
            Button button = buttonObj.GetComponent<Button>();

            if (isUnlocked)
            {
                label.text = option.optionText;
                button.interactable = true;
                button.onClick.AddListener(() => currentNPC.ChooseOption(option)); // updates npc depending on chosen dialouge
            }

            else // else dont allow to interact
            {
                label.text = "[MORE CLUES NEEDED]";
                button.interactable = false;

            }
        }
    }

    public void CloseDialogue()
    {
        UIStateManager.Instance.ClosePanel(dialoguePanel); // hide panel
        currentNPC = null;

    }

    // for wrong outfit
    public void ShowDismissal(string line)
    {
        UIStateManager.Instance.OpenPanel(dialoguePanel);
        npcLineText.text = line;

        foreach (Transform child in optionsContainer)
        {
            Destroy(child.gameObject);
        }

        GameObject buttonObj = Instantiate(optionButtonPrefab, optionsContainer);
        TextMeshProUGUI label = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
        Button button = buttonObj.GetComponent<Button>();

        label.text = "Leave";
        button.onClick.AddListener(CloseDialogue);

    }
}