using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoad : MonoBehaviour
{
    public SaveSlot[] SaveSlots;
    public SaveLoadInfo SaveInfo;
    public Transform SaveNamesFolder;
    public GameObject DefaultButton;
    public Toggle RequireConfirmation;
    //public Toggle SkipPreviewForHugeFiles; To be implemented later

    private void Start()
    {
        RequireConfirmation.isOn = PlayerPrefs.GetInt("SaveLoadConfirmation", 0) == 1 ? true : false;           
    }

    public void ConfirmationChanged()
    {
        PlayerPrefs.SetInt("SaveLoadConfirmation", RequireConfirmation.isOn ? 1 : 0);
    }

    public void InputChanged()
    {
        SaveInfo.LoadGame.interactable = false;
        SaveInfo.DeleteSave.interactable = false;
        try
        {
            if (!File.Exists($"{State.SaveDirectory}{SaveInfo.SavedGameName.text}.sav"))
            {
                SaveInfo.LeftText.text = "No file with this name";
                SaveInfo.RightText.text = "";
                SaveInfo.LoadGame.interactable = false;
                return;
            }
            SaveInfo.DeleteSave.interactable = true;
            World TempWorld = State.PreviewSave($"{State.SaveDirectory}{SaveInfo.SavedGameName.text}.sav");
            if (TempWorld == null)
            {
                SaveInfo.LeftText.text = "Invalid save (from a incompatible version?)";
                SaveInfo.LoadGame.interactable = false;
                return;
            }
            SaveInfo.LoadGame.interactable = true;
            if (TempWorld.Empires != null)
                TempWorld.MainEmpires = TempWorld.Empires.ToList();
            
            if (TempWorld.MainEmpires == null)
            {
                SaveInfo.LeftText.text = "Is a pure tactical game";
                StringBuilder sb = new StringBuilder();
                int livingAttackers = TempWorld.TacticalData.armies[0].Units.Where(s => s.IsDead == false).Count();
                int livingDefenders = TempWorld.TacticalData.units.Where(s => s.Unit.IsDead == false).Count() - livingAttackers;
                sb.AppendLine($"Tactical Turn : {TempWorld.TacticalData.currentTurn}");
                sb.AppendLine($"Attacker: {TempWorld.TacticalData.AttackerName} Living Units: {livingAttackers}");
                sb.AppendLine($"Defender: {TempWorld.TacticalData.DefenderName} Living Units: {livingDefenders}");
                sb.AppendLine($"Saved date: {File.GetLastWriteTime($"{State.SaveDirectory}{SaveInfo.SavedGameName.text}.sav")}");
                sb.AppendLine($"Saved version: {(!string.IsNullOrWhiteSpace(TempWorld.SaveVersion) ? TempWorld.SaveVersion : "Pre V08D")}");
                SaveInfo.RightText.text = sb.ToString();
                return;
            }
            StringBuilder sbLeft = new StringBuilder();
            StringBuilder sbRight = new StringBuilder();
            foreach (Empire empire in TempWorld.MainEmpires)
            {
                int villageCount = TempWorld.Villages.Where(s => s.Side == empire.Side).Count();
                int armyCount = empire.Armies.Count();
                if (armyCount > 0 || villageCount > 0)
                    sbLeft.AppendLine($"{(empire.Name != null ? empire.Name : empire.Race.ToString())} - Villages: {villageCount} Armies: {armyCount}");
            }
            sbRight.AppendLine($"Strategic Turn: {TempWorld.Turn}");
            if (TempWorld.TacticalData != null)
            {
                sbRight.AppendLine("Game is in the middle of a battle");
                sbRight.AppendLine($"Tactical Turn : {TempWorld.TacticalData.currentTurn}");
            }
            sbRight.AppendLine($"Saved date: {File.GetLastWriteTime($"{State.SaveDirectory}{SaveInfo.SavedGameName.text}.sav")}");
            sbRight.AppendLine($"Saved version: {(!string.IsNullOrWhiteSpace(TempWorld.SaveVersion) ? TempWorld.SaveVersion : "Pre V08D")}");
            SaveInfo.LeftText.text = sbLeft.ToString();
            SaveInfo.RightText.text = sbRight.ToString();

        }
        catch
        {
            SaveInfo.LeftText.text = "Error previewing the save file";
        }
    }

    public void Save()
    {
        State.Save($"{State.SaveDirectory}{SaveInfo.SavedGameName.text}.sav");
        gameObject.SetActive(false);
        State.GameManager.CloseMenu();
    }

    public void Load()
    {
        if (RequireConfirmation.isOn && State.GameManager.CurrentScene != State.GameManager.Start_Mode)
        {
            var box = State.GameManager.CreateDialogBox();
            box.SetData(LoadSave, "Load Game", "Cancel", "You already are in a game, are you sure you want to load?");
        }
        else
        {
            LoadSave();
        }
        
    }

    private void LoadSave()
    {
        State.Load($"{State.SaveDirectory}{SaveInfo.SavedGameName.text}.sav");
        gameObject.SetActive(false);
        State.GameManager.CloseMenu();
    }

    public void DeleteSave()
    {
        var box = State.GameManager.CreateDialogBox();
        box.SetData(ActuallyDelete, "Delete", "Cancel", "Are you sure you want to delete this saved game?");
    }

    void ActuallyDelete()
    {
        if (File.Exists($"{State.SaveDirectory}{SaveInfo.SavedGameName.text}.sav"))
            File.Delete($"{State.SaveDirectory}{SaveInfo.SavedGameName.text}.sav");
        InputChanged();
        ListSlots();
    }

    internal void ListSlots()
    {
        int children = SaveNamesFolder.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(SaveNamesFolder.GetChild(i).gameObject);
        }
        BuildFiles(State.SaveDirectory, "sav");
        bool InMainMenu = State.GameManager.CurrentScene == State.GameManager.Start_Mode;
        SaveInfo.SaveGame.interactable = !InMainMenu;
        InputChanged();
    }

    private void BuildFiles(string directory, string extension)
    {
        if (Directory.Exists(directory) == false)
            Directory.CreateDirectory(directory);
        string[] files = Directory.GetFiles(directory);

        foreach (string file in files)
        {
            if (!File.Exists(file)) continue;

            if (CompatibleFileExtension(file, extension))
            {
                var filename = Path.GetFileNameWithoutExtension(file);

                var button = Instantiate(DefaultButton, SaveNamesFolder).GetComponent<Button>();
                button.GetComponentInChildren<Text>().text = $"{filename}";
                button.GetComponent<Button>().onClick.AddListener(() => SaveInfo.SavedGameName.text = filename);
            }
        }
    }

    public bool CompatibleFileExtension(string file, string extension)
    {
        if (extension.Length == 0)
        {
            return true;
        }

        if (file.EndsWith("." + extension))
        {
            return true;
        }

        // Not found, return not compatible
        return false;
    }


}


