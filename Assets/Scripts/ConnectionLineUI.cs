using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ConnectionLineUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private string contextReason; // contect clue on line

    // sets contect clue box
    private static GameObject tooltip;
    private static TextMeshProUGUI tooltipText;

    // assigns context box to line
    public static void Init(GameObject tooltipObject, TextMeshProUGUI text)
    {
        tooltip = tooltipObject;
        tooltipText = text;
    }

    // assigns context text 
    public void Setup(string reason)
    {
        contextReason = reason;
    }

    public void OnPointerEnter(PointerEventData eventData) // on mouse hover
    {
        tooltipText.text = contextReason; // set context text to display context clue
        tooltip.transform.position = transform.position; // center of the line
        tooltip.SetActive(true); // show context box
    }

    public void OnPointerExit(PointerEventData eventData) // on mouse leave
    {
        tooltip.SetActive(false);  // hide context
    }
}