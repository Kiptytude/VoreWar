using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SliderRevealer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Slider Slider;

    bool hovering;

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovering = false;
    }

    void TrySetValue(string fl)
    {
        if (float.TryParse(fl, out float result))
            Slider.value = result;
    }

    // Use this for initialization
    void Start()
    {
        Slider = GetComponentInParent<Slider>();
        if (Slider == null)
            Debug.LogWarning("SliderRevealer has no slider!");
    }

    // Update is called once per frame
    void Update()
    {
        if (hovering && Slider.interactable)
        {
            State.GameManager.HoveringTooltip.UpdateInformation(Slider); 
            
            if (Input.GetMouseButton(1))
            {
                var box = Instantiate(State.GameManager.InputBoxPrefab).GetComponentInChildren<InputBox>();
                box.SetData(TrySetValue, "Set Value", "Cancel", $"Change the value of this slider?  Allowed Range {Slider.minValue} - {Slider.maxValue}", 4);
            }
        }
    }
}
