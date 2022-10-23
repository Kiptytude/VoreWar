using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleReportPanel : MonoBehaviour
{
    public TextMeshProUGUI AttackerText;
    public TextMeshProUGUI DefenderText;

    public Button WatchBattle;
    public Button SkipBattle;
    public Button PlayBattle;

    public Button SkipBattleStats;

    private void Start()
    {
        WatchBattle.onClick.AddListener(() => State.GameManager.ActivateQueuedTacticalMode(GameManager.PreviewSkip.Watch));
        PlayBattle.onClick.AddListener(() => State.GameManager.ActivateQueuedTacticalMode(GameManager.PreviewSkip.Watch));
        SkipBattle.onClick.AddListener(() => State.GameManager.ActivateQueuedTacticalMode(GameManager.PreviewSkip.SkipNoStats));
        SkipBattleStats.onClick.AddListener(() => State.GameManager.ActivateQueuedTacticalMode(GameManager.PreviewSkip.SkipWithStats));
    }

    private void Update()
    {
        if (gameObject.activeSelf && Input.GetButton("Submit"))
        {
            State.GameManager.ActivateQueuedTacticalMode(GameManager.PreviewSkip.Watch);
        }
    }

    internal void Activate(Village village, Army invader, Army defender)
    {
        gameObject.SetActive(true);

        int defenderSide = defender?.Side ?? village.Side;
        TacticalAIType defenderType = State.World.GetEmpireOfSide(defenderSide).TacticalAIType;
        if (village != null && State.World.GetEmpireOfSide(defenderSide).TacticalAIType == TacticalAIType.None)
            defenderType = TacticalAIType.None;

        Empire attackerEmpire = State.World.GetEmpireOfSide(invader.Side);
        if (attackerEmpire.TacticalAIType == TacticalAIType.None || defenderType == TacticalAIType.None)
        {
            WatchBattle.gameObject.SetActive(false);
            SkipBattle.gameObject.SetActive(false);
            SkipBattleStats.gameObject.SetActive(false);
            PlayBattle.gameObject.SetActive(true);
        }
        else
        {
            WatchBattle.gameObject.SetActive(true);
            SkipBattle.gameObject.SetActive(true);
            SkipBattleStats.gameObject.SetActive(true);
            PlayBattle.gameObject.SetActive(false);
        }

        State.GameManager.PipCamera.SetLocation(invader.Position.x, invader.Position.y, 5);
        var sb = State.GameManager.StrategyMode.ArmyToolTip(invader);
        AttackerText.text = sb.ToString();
        sb = new System.Text.StringBuilder();
        if (defender != null)
            sb = State.GameManager.StrategyMode.ArmyToolTip(defender);
        if (village != null)
        {
            sb.AppendLine($"Village: {village.Name}");
            if (village.Capital)
                sb.AppendLine($"Capital City ({village.OriginalRace})");
            sb.AppendLine($"Owner: {(Race)village.Side}");
            sb.AppendLine($"Race: {village.Race}");
            sb.AppendLine($"Population: {village.Population}");
            sb.AppendLine($"Garrison: {village.Garrison}");
        }
        DefenderText.text = sb.ToString();
    }

}

