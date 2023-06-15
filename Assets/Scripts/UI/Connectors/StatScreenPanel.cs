using UnityEngine;
using UnityEngine.UI;

public class StatScreenPanel : MonoBehaviour
{
    public Text VictoryType;
    public Text AttackerTitle;
    public Text DefenderTitle;
    public Text AttackerText;
    public Text DefenderText;
    public Text CloseButtonText;
    public Button ExtendTime;

    internal bool AutoClose;
    internal float AutoCloseTime;

    public void Open(bool AIOnly)
    {
        State.GameManager.StatScreen.gameObject.SetActive(true);
        if (Config.TimedAIStats && AIOnly)
        {
            State.GameManager.StatScreen.AutoClose = true;
            State.GameManager.StatScreen.AutoCloseTime = 10;
            ExtendTime.gameObject.SetActive(true);
        }
        else
        {
            State.GameManager.StatScreen.AutoClose = false;
            State.GameManager.StatScreen.AutoCloseTime = 0;
            CloseButtonText.text = "Exit Stats Screen";
            ExtendTime.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (AutoClose && AutoCloseTime > 0)
        {
            AutoCloseTime -= Time.deltaTime;
            if (AutoCloseTime < 0)
                State.GameManager.CloseStatsScreen();
            CloseButtonText.text = $"Exit Stats Screen (automatically happens in {(int)AutoCloseTime})";
        }

    }

    public void AddTime()
    {
        AutoCloseTime += 15;
    }

}
