using UnityEngine;
using UnityEngine.EventSystems;

public class MapEditorMisc : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public MapEditor.SpecialType Type;

    public void OnPointerClick(PointerEventData eventData)
    {
        State.GameManager.MapEditor.SetMiscType(Type, transform);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        State.GameManager.MapEditor.SetMiscTooltip(Type);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        State.GameManager.MapEditor.SetBlankTooltip();
    }
}

