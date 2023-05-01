using UnityEngine;
using UnityEngine.EventSystems;

public class MapEditorDoodad : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public StrategicDoodadType type;
    public void OnPointerClick(PointerEventData eventData)
    {
        State.GameManager.MapEditor.SetDoodadType(type, transform);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        State.GameManager.MapEditor.SetDoodadTooltip(type);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        State.GameManager.MapEditor.SetBlankTooltip();
    }
}
