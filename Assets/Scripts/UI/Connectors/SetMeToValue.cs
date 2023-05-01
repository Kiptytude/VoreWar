using UnityEngine;
using UnityEngine.UI;

public class SetMeToValue : MonoBehaviour
{
    public void Set(Slider slider)
    {
        GetComponent<Text>().text = slider.value.ToString();
    }
}
