using UnityEngine;
using UnityEngine.EventSystems;

public class MapEditorVillage : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Race Race;
    public void OnPointerClick(PointerEventData eventData)
    {
        State.GameManager.MapEditor.SetVillageType(Race, transform);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        State.GameManager.MapEditor.SetVillageTooltip(Race);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        State.GameManager.MapEditor.SetBlankTooltip();
    }
}

