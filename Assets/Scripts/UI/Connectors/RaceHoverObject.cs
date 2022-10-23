using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class RaceHoverObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool hovering;
    internal Race race;
    
    

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