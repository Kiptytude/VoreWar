using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class VariableScreenTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string text;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (State.GameManager.VariableEditor.gameObject.activeSelf)
            State.GameManager.VariableEditor.ChangeToolTip(text);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (State.GameManager.VariableEditor.gameObject.activeSelf)
            State.GameManager.VariableEditor.ChangeToolTip("");

    }
}