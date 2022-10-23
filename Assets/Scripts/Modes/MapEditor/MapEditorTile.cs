using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MapEditorTile : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public StrategicTileType type;
    public void OnPointerClick(PointerEventData eventData)
    {
        State.GameManager.MapEditor.SetTileType(type, transform);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        State.GameManager.MapEditor.SetTileTooltip(type);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        State.GameManager.MapEditor.SetBlankTooltip();
    }
}
