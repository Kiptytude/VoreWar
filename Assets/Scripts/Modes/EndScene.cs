using UnityEngine;
using UnityEngine.UI;

public class EndScene : SceneBase
{
    public Text EndText;
    public Text Text;

    public RectTransform TextWindow;
    public void Win(string race)
    {
        EndText.text = $"The {race} are victorious";
        Text.text = State.World.Stats.Summary();
    }

    public void Lose(Race race)
    {
        EndText.text = $"The {race} have been eradicated";
        Text.text = State.World.Stats.Summary();
    }

    public override void CleanUp()
    {
        State.GameManager.StrategyMode.ClearData();
    }

    public override void ReceiveInput()
    {
    }
}
