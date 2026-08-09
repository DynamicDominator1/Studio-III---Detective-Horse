using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ConnectionLineUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private string contextReason;
    private static GameObject tooltip;
    private static TextMeshProUGUI tooltipText;

    public static void Init(GameObject tooltipObject, TextMeshProUGUI text)
    {
        tooltip = tooltipObject;
        tooltipText = text;
    }

    public void Setup(string reason)
    {
        contextReason = reason;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltipText.text = contextReason;
        tooltip.transform.position = transform.position; // center of the line
        tooltip.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.SetActive(false);
    }
}