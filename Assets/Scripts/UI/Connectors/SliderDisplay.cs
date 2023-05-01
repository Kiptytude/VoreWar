using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Connectors
{
    public class SliderDisplay : MonoBehaviour
    {
        public TextMeshProUGUI DisplayText;
        public Slider Slider;

        private void Start()
        {
            DisplayText.text = Math.Round(Slider.value, 3).ToString();
        }

        public void UpdateValue()
        {
            DisplayText.text = Math.Round(Slider.value, 3).ToString();
        }
    }
}
