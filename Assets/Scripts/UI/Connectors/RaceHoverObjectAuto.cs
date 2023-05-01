using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class RaceHoverObjectAuto : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool hovering;

    Race race;

    private void Start()
    {
        var comp = GetComponentInChildren<TextMeshProUGUI>();
        if (comp == null)
        {
            Destroy(this);
            return;
        }

        if (Enum.TryParse(GetComponentInChildren<TextMeshProUGUI>().text, out Race result))
            race = result;
        else
            Destroy(this);
    }
    private void Update()
    {
        if (hovering == false)
            return;
        State.GameManager.HoveringRacePicture.UpdateInformation(race);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovering = false;
    }
}