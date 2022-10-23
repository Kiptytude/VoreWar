using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class EventScreen : MonoBehaviour
{
    public TextMeshProUGUI MainText;
    public TextMeshProUGUI RaceText;

    public Button FirstChoice;
    public Button SecondChoice;
    public Button ThirdChoice;

    private void Update()
    {
        if (gameObject.activeSelf == false)
            return;

        if (Input.GetKeyDown(KeyCode.Alpha1) && FirstChoice.interactable)
            FirstChoice.onClick.Invoke();
        else if (Input.GetKeyDown(KeyCode.Alpha2) && SecondChoice.interactable)
            SecondChoice.onClick.Invoke();
        else if (Input.GetKeyDown(KeyCode.Alpha3) && ThirdChoice.interactable)
            ThirdChoice.onClick.Invoke();
    }

    public void OnDisable() //So that gold updates properly after this window is closed
    {
        if (State.GameManager != null)
            MiscUtilities.DelayedInvoke(State.GameManager.StrategyMode.Regenerate, .1f);
    }

}
