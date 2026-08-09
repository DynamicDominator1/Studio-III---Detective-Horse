using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI Instance;

    public GameObject dialoguePanel; // the whole Dialogue Tab panel
    public TextMeshProUGUI npcLineText;
    public Transform optionsContainer;
    public GameObject optionButtonPrefab;

    private NPC currentNPC;

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
        dialoguePanel.SetActive(false);
    }

    public void OpenDialogue(NPC npc, string line, List<Dialogue> options)
    {
        currentNPC = npc;
        UIStateManager.Instance.OpenPanel(dialoguePanel);
        UpdateDialogue(line, options);
    }

    public void UpdateDialogue(string line, List<Dialogue> options)
    {
        npcLineText.text = line;

        foreach (Transform child in optionsContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (Dialogue option in options)
        {
            bool isUnlocked = option.requiredClue == null || InventoryManager.Instance.collectedClues.Contains(option.requiredClue);

            GameObject buttonObj = Instantiate(optionButtonPrefab, optionsContainer);
            TextMeshProUGUI label = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
            Button button = buttonObj.GetComponent<Button>();

            if (isUnlocked)
            {
                label.text = option.optionText;
                button.interactable = true;
                button.onClick.AddListener(() => currentNPC.ChooseOption(option));
            }
            else
            {
                label.text = "[MORE CLUES NEEDED]";
                button.interactable = false;
            }
        }
    }

    public void CloseDialogue()
    {
        UIStateManager.Instance.ClosePanel(dialoguePanel);
        currentNPC = null;
    }

    public void ShowDismissal(string npcName, string line)
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