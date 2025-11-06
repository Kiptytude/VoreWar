using System;
using UnityEngine;
using UnityEngine.UI;


public class Options : MonoBehaviour
{
    public Slider StrategicDelaySlider;
    public Slider TacticalPlayerMovementDelaySlider;
    public Slider TacticalAIMovementDelaySlider;
    public Slider TacticalFriendlyAIMovementDelay;
    public Slider TacticalAttackDelaySlider;
    public Slider TacticalVoreDelaySlider;
    public Dropdown AutoAdvance;
    public Dropdown WatchAIBattles;
    public Dropdown AllianceSquares;
    public Toggle IgnoreMonsterBattles;
    public Toggle StopAtEndOfBattle;
    public Toggle DamageNumbers;
    public Toggle SoundEnabled;
    public Slider CombatSoundVolume;
    public Slider VoreSoundVolume;
    public Slider PassiveVoreSoundVolume;
    public Toggle CloseInDigestionNoises;
    public Toggle RunInBackground;
    public Toggle ShowLevelText;
    public Toggle AltFriendlyColor;
    public Toggle PromptEndTurn;
    public Toggle ScrollToBattleLocation;

    public Slider BannerScale;

    public Dropdown StrategicMoveCamera;
    public Dropdown TacticalMoveCamera;
    public Toggle Notifications;
    public Toggle ExtraTacticalInfo;

    public Toggle SimpleFarms;
    public Toggle SimpleForests;
    public Toggle HideUnitViewer;
    public Toggle HideBaseStats;

    public Toggle RightClickMenu;
    public Toggle DesaturatedTiles;

    public Toggle AutoUseAI;

    public Toggle EdgeScrolling;
    public Toggle HardLava;

    public Text TooltipText;


    public void Open()
    {
        GetValues();
    }

    public void CloseAndSave()
    {
        SetNewValues();
        if (State.GameManager.CurrentScene == State.GameManager.StrategyMode)
            State.GameManager.StrategyMode.RedrawTiles();
        gameObject.SetActive(false);
    }

    public void AskClearAllSettings()
    {
        var box = Instantiate(State.GameManager.DialogBoxPrefab).GetComponent<DialogBox>();
        box.SetData(ClearAllSettings, "Delete them all", "Cancel", "This clears all saved settings in options and content menus, are you sure you want to do this?");
    }

    void ClearAllSettings()
    {
        PlayerPrefs.DeleteAll();
        GetValues();
    }


    public void ResetUserdata()
    {
        var box = Instantiate(State.GameManager.DialogBoxPrefab).GetComponent<DialogBox>();
        box.SetData(ClearUserdata, "Yes, wipe Userdata", "Cancel", "This deletes the 'Userdata' folder and restores it to a default state. This will delete ALL, saves, maps, saved strategic presets, race edit presets, namelists, uniform, unit customizations, custom traits, events, and, unit tags. Are you SURE you want to do this? This cannot be undone. (Will take some time to complete)");
    }

    void ClearUserdata()
    {
        State.WipeUserdata();
    }

    public void ResetNamelists()
    {
        var box = Instantiate(State.GameManager.DialogBoxPrefab).GetComponent<DialogBox>();
        box.SetData(ClearLists, "Yes, reset namelists", "Cancel", "This deletes all namelists from the 'Userdata' folder and restores them to defaults. Are you sure you want to do this? This cannot be undone. (Will take a moment to complete)");
    }

    void ClearLists()
    {
        State.ResetNamelists();
    }

    public void UpdateNamelists()
    {
        var box = Instantiate(State.GameManager.DialogBoxPrefab).GetComponent<DialogBox>();
        box.SetData(UpdateLists, "Update namelists", "Cancel", "This reloads all namelists from the 'Userdata' folder, immediately updating them in-game.");
    }

    void UpdateLists()
    {
        State.ReloadNamelists();
    }

    void GetValues()
    {
        StrategicDelaySlider.value = PlayerPrefs.GetFloat("StrategicDelaySlider", .25f);
        TacticalPlayerMovementDelaySlider.value = PlayerPrefs.GetFloat("TacticalPlayerMovementDelaySlider", .1f);
        TacticalAIMovementDelaySlider.value = PlayerPrefs.GetFloat("TacticalAIMovementDelaySlider", .2f);
        TacticalFriendlyAIMovementDelay.value = PlayerPrefs.GetFloat("TacticalFriendlyAIMovementDelay", PlayerPrefs.GetFloat("TacticalAIMovementDelaySlider", .2f));
        TacticalAttackDelaySlider.value = PlayerPrefs.GetFloat("TacticalAttackDelaySlider", .3f);
        TacticalVoreDelaySlider.value = PlayerPrefs.GetFloat("TacticalVoreDelaySlider", .3f);
        AutoAdvance.value = PlayerPrefs.GetInt("AutoAdvance", 1);
        StrategicMoveCamera.value = PlayerPrefs.GetInt("StrategicMoveCamera", 0);
        TacticalMoveCamera.value = PlayerPrefs.GetInt("TacticalMoveCamera", 0);
        DamageNumbers.isOn = PlayerPrefs.GetInt("DamageNumbers", 1) == 1;
        ExtraTacticalInfo.isOn = PlayerPrefs.GetInt("ExtraTacticalInfo", 0) == 1;
        IgnoreMonsterBattles.isOn = PlayerPrefs.GetInt("IgnoreMonsterBattles", 0) == 1;
        SoundEnabled.isOn = PlayerPrefs.GetInt("SoundEnabled", 1) == 1;
        WatchAIBattles.value = PlayerPrefs.GetInt("WatchAI", 0);
        CombatSoundVolume.value = PlayerPrefs.GetFloat("CombatSoundVolume", .75f);
        VoreSoundVolume.value = PlayerPrefs.GetFloat("VoreSoundVolume", .75f);
        BannerScale.value = PlayerPrefs.GetFloat("BannerScale", 5);
        PassiveVoreSoundVolume.value = PlayerPrefs.GetFloat("PassiveVoreSoundVolume", .75f);
        CloseInDigestionNoises.isOn = PlayerPrefs.GetInt("CloseInDigestionNoises", 0) == 1;
        StopAtEndOfBattle.isOn = PlayerPrefs.GetInt("StopAtEndOfBattle", 0) == 1;
        RunInBackground.isOn = PlayerPrefs.GetInt("RunInBackground", 0) == 1;
        ShowLevelText.isOn = PlayerPrefs.GetInt("ShowLevelText", 1) == 1;
        AltFriendlyColor.isOn = PlayerPrefs.GetInt("AltFriendlyColor", 0) == 1;
        PromptEndTurn.isOn = PlayerPrefs.GetInt("PromptEndTurn", 1) == 1;
        ScrollToBattleLocation.isOn = PlayerPrefs.GetInt("ScrollToBattleLocation", 1) == 1;
        Notifications.isOn = PlayerPrefs.GetInt("Notifications", 1) == 1;
        SimpleFarms.isOn = PlayerPrefs.GetInt("SimpleFarms", 0) == 1;
        SimpleForests.isOn = PlayerPrefs.GetInt("SimpleForests", 0) == 1;
        HideUnitViewer.isOn = PlayerPrefs.GetInt("HideUnitViewer", 0) == 1;
        RightClickMenu.isOn = PlayerPrefs.GetInt("RightClickMenu", 0) == 1;
        DesaturatedTiles.isOn = PlayerPrefs.GetInt("DesaturatedTiles", 0) == 1;
        HideBaseStats.isOn = PlayerPrefs.GetInt("HideBaseStats", 0) == 1;
        EdgeScrolling.isOn = PlayerPrefs.GetInt("EdgeScrolling", 0) == 1;
        HardLava.isOn = PlayerPrefs.GetInt("HardLava", 0) == 1;
        AutoUseAI.isOn = PlayerPrefs.GetInt("AutoUseAI", 0) == 1;
        AllianceSquares.value = PlayerPrefs.GetInt("AllianceSquares", 1);

        LoadFromStored();
    }

    public void RefreshText()
    {
        StrategicDelaySlider.GetComponentInChildren<Text>().text = $"{StrategicDelaySlider.name}: {Math.Round(StrategicDelaySlider.value, 3)} sec";
        TacticalPlayerMovementDelaySlider.GetComponentInChildren<Text>().text = $"{TacticalPlayerMovementDelaySlider.name}: {Math.Round(TacticalPlayerMovementDelaySlider.value, 3)} sec";
        TacticalAIMovementDelaySlider.GetComponentInChildren<Text>().text = $"{TacticalAIMovementDelaySlider.name}: {Math.Round(TacticalAIMovementDelaySlider.value, 3)} sec";
        TacticalFriendlyAIMovementDelay.GetComponentInChildren<Text>().text = $"{TacticalFriendlyAIMovementDelay.name}: {Math.Round(TacticalFriendlyAIMovementDelay.value, 3)} sec";
        TacticalAttackDelaySlider.GetComponentInChildren<Text>().text = $"{TacticalAttackDelaySlider.name}: {Math.Round(TacticalAttackDelaySlider.value, 3)} sec";
        TacticalVoreDelaySlider.GetComponentInChildren<Text>().text = $"{TacticalVoreDelaySlider.name}: {Math.Round(TacticalVoreDelaySlider.value, 3)} sec";
    }

    void SetNewValues()
    {
        PlayerPrefs.SetFloat("StrategicDelaySlider", StrategicDelaySlider.value);
        PlayerPrefs.SetFloat("TacticalPlayerMovementDelaySlider", TacticalPlayerMovementDelaySlider.value);
        PlayerPrefs.SetFloat("TacticalAIMovementDelaySlider", TacticalAIMovementDelaySlider.value);
        PlayerPrefs.SetFloat("TacticalFriendlyAIMovementDelay", TacticalFriendlyAIMovementDelay.value);
        PlayerPrefs.SetFloat("TacticalAttackDelaySlider", TacticalAttackDelaySlider.value);
        PlayerPrefs.SetFloat("TacticalVoreDelaySlider", TacticalVoreDelaySlider.value);
        PlayerPrefs.SetInt("AutoAdvance", AutoAdvance.value);
        PlayerPrefs.SetInt("StrategicMoveCamera", StrategicMoveCamera.value);
        PlayerPrefs.SetInt("TacticalMoveCamera", TacticalMoveCamera.value);
        PlayerPrefs.SetInt("AllianceSquares", AllianceSquares.value);
        PlayerPrefs.SetInt("IgnoreMonsterBattles", IgnoreMonsterBattles.isOn ? 1 : 0);
        PlayerPrefs.SetInt("SoundEnabled", SoundEnabled.isOn ? 1 : 0);
        PlayerPrefs.SetInt("DamageNumbers", DamageNumbers.isOn ? 1 : 0);
        PlayerPrefs.SetInt("ExtraTacticalInfo", ExtraTacticalInfo.isOn ? 1 : 0);
        PlayerPrefs.SetInt("CloseInDigestionNoises", CloseInDigestionNoises.isOn ? 1 : 0);
        PlayerPrefs.SetInt("StopAtEndOfBattle", StopAtEndOfBattle.isOn ? 1 : 0);
        PlayerPrefs.SetInt("RunInBackground", RunInBackground.isOn ? 1 : 0);
        PlayerPrefs.SetInt("ShowLevelText", ShowLevelText.isOn ? 1 : 0);
        PlayerPrefs.SetInt("AltFriendlyColor", AltFriendlyColor.isOn ? 1 : 0);
        PlayerPrefs.SetInt("PromptEndTurn", PromptEndTurn.isOn ? 1 : 0);
        PlayerPrefs.SetInt("ScrollToBattleLocation", ScrollToBattleLocation.isOn ? 1 : 0);
        PlayerPrefs.SetInt("Notifications", Notifications.isOn ? 1 : 0);
        PlayerPrefs.SetInt("SimpleFarms", SimpleFarms.isOn ? 1 : 0);
        PlayerPrefs.SetInt("SimpleForests", SimpleForests.isOn ? 1 : 0);
        PlayerPrefs.SetInt("HideUnitViewer", HideUnitViewer.isOn ? 1 : 0);
        PlayerPrefs.SetInt("RightClickMenu", RightClickMenu.isOn ? 1 : 0);
        PlayerPrefs.SetInt("DesaturatedTiles", DesaturatedTiles.isOn ? 1 : 0);
        PlayerPrefs.SetInt("HideBaseStats", HideBaseStats.isOn ? 1 : 0);
        PlayerPrefs.SetInt("EdgeScrolling", EdgeScrolling.isOn ? 1 : 0);
        PlayerPrefs.SetInt("HardLava", HardLava.isOn ? 1 : 0);
        PlayerPrefs.SetInt("AutoUseAI", AutoUseAI.isOn ? 1 : 0);
        PlayerPrefs.SetInt("WatchAI", WatchAIBattles.value);
        PlayerPrefs.SetFloat("CombatSoundVolume", CombatSoundVolume.value);
        PlayerPrefs.SetFloat("VoreSoundVolume", VoreSoundVolume.value);
        PlayerPrefs.SetFloat("PassiveVoreSoundVolume", PassiveVoreSoundVolume.value);
        PlayerPrefs.SetFloat("BannerScale", BannerScale.value);
        LoadFromStored();
        PlayerPrefs.Save();

    }

    public void LoadFromStored()
    {
        Config.StrategicAIMoveDelay = PlayerPrefs.GetFloat("StrategicDelaySlider", .25f);
        Config.TacticalPlayerMovementDelay = PlayerPrefs.GetFloat("TacticalPlayerMovementDelaySlider", .1f);
        Config.TacticalAIMovementDelay = PlayerPrefs.GetFloat("TacticalAIMovementDelaySlider", .1f);
        Config.TacticalFriendlyAIMovementDelay = PlayerPrefs.GetFloat("TacticalFriendlyAIMovementDelay", .1f);
        Config.TacticalAttackDelay = PlayerPrefs.GetFloat("TacticalAttackDelaySlider", .2f);
        Config.TacticalVoreDelay = PlayerPrefs.GetFloat("TacticalVoreDelaySlider", .3f);
        Config.BannerScale = .5f + PlayerPrefs.GetFloat("BannerScale", 1f) / 10;
        State.GameManager.SoundManager.SetVolume(PlayerPrefs.GetFloat("CombatSoundVolume", .75f), PlayerPrefs.GetFloat("VoreSoundVolume", .75f), PlayerPrefs.GetFloat("PassiveVoreSoundVolume", .75f));
        Config.AutoAdvance = (Config.AutoAdvanceType)PlayerPrefs.GetInt("AutoAdvance", 1);
        Config.DisplayEndOfTurnText = PlayerPrefs.GetInt("DisplayEndOfTurnText", 0) == 1;
        Config.AllianceSquaresDarkness = PlayerPrefs.GetInt("AllianceSquares", 1);
        Config.IgnoreMonsterBattles = PlayerPrefs.GetInt("IgnoreMonsterBattles", 0) == 1;
        State.GameManager.SoundManager.SoundEnabled = PlayerPrefs.GetInt("SoundEnabled", 1) == 1;
        Config.DamageNumbers = PlayerPrefs.GetInt("DamageNumbers", 1) == 1;
        Config.ExtraTacticalInfo = PlayerPrefs.GetInt("ExtraTacticalInfo", 0) == 1;
        Config.WatchAIBattles = PlayerPrefs.GetInt("WatchAI") >= 2;
        Config.ShowStatsForSkippedBattles = PlayerPrefs.GetInt("WatchAI", 1) == 1;
        Config.SkipAIOnlyStats = PlayerPrefs.GetInt("WatchAI", 1) == 3;
        Config.TimedAIStats = PlayerPrefs.GetInt("WatchAI", 1) == 4;
        Config.BattleReport = PlayerPrefs.GetInt("WatchAI", 1) == 5;
        Config.StrategicCenterCameraOnAction = PlayerPrefs.GetInt("StrategicMoveCamera", 0) == 1;
        Config.TacticalCenterCameraOnAction = PlayerPrefs.GetInt("TacticalMoveCamera", 0) == 1;
        Config.StrategicCameraActionPanel = PlayerPrefs.GetInt("StrategicMoveCamera", 0) == 2;
        Config.TacticalCameraActionPanel = PlayerPrefs.GetInt("TacticalMoveCamera", 0) == 2;
        Application.runInBackground = PlayerPrefs.GetInt("RunInBackground", 0) == 1;
        Config.CloseInDigestionNoises = PlayerPrefs.GetInt("CloseInDigestionNoises", 0) == 1;
        Config.StopAtEndOfBattle = PlayerPrefs.GetInt("StopAtEndOfBattle", 0) == 1;
        Config.ShowLevelText = PlayerPrefs.GetInt("ShowLevelText", 1) == 1;
        Config.AltFriendlyColor = PlayerPrefs.GetInt("AltFriendlyColor", 0) == 1;
        Config.Notifications = PlayerPrefs.GetInt("Notifications", 0) == 1;
        Config.SimpleFarms = PlayerPrefs.GetInt("SimpleFarms", 0) == 1;
        Config.SimpleForests = PlayerPrefs.GetInt("SimpleForests", 0) == 1;
        Config.HideUnitViewer = PlayerPrefs.GetInt("HideUnitViewer", 0) == 1;
        Config.RightClickMenu = PlayerPrefs.GetInt("RightClickMenu", 0) == 1;
        Config.DesaturatedTiles = PlayerPrefs.GetInt("DesaturatedTiles", 0) == 1;
        Config.HideBaseStats = PlayerPrefs.GetInt("HideBaseStats", 0) == 1;
        Config.EdgeScrolling = PlayerPrefs.GetInt("EdgeScrolling", 0) == 1;
        Config.HardLava = PlayerPrefs.GetInt("HardLava", 0) == 1;
        Config.AutoUseAI = PlayerPrefs.GetInt("AutoUseAI", 0) == 1;
        Config.PromptEndTurn = PlayerPrefs.GetInt("PromptEndTurn", 1) == 1;
        Config.ScrollToBattleLocation = PlayerPrefs.GetInt("ScrollToBattleLocation", 1) == 1;
    }

    internal void ChangeToolTip(int value)
    {
        TooltipText.text = DefaultTooltips.Tooltip(value);
    }

    public void SoundEnabledChanged()
    {
        CombatSoundVolume.interactable = SoundEnabled.isOn;
        PassiveVoreSoundVolume.interactable = SoundEnabled.isOn;
        VoreSoundVolume.interactable = SoundEnabled.isOn;
        CloseInDigestionNoises.interactable = SoundEnabled.isOn;
    }

}

