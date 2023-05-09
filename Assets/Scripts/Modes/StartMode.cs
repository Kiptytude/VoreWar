using OdinSerializer.Utilities;
using UnityEngine;
using UnityEngine.UI;

public class StartMode : SceneBase
{
    public Text versionNumber;
    public Text miscText;

    public Button Preset1;
    public Button Preset2;
    public Button Preset3;

    public void Start()
    {
        State.GameManager.Menu.Options.LoadFromStored();
        State.GameManager.Menu.CheatMenu.LoadFromStored();
        versionNumber.text = $"Version: {State.Version}";
        State.GameManager.Menu.ContentSettings.Refresh();
        Preset1.onClick.AddListener(() => SetPreset(1));
        Preset2.onClick.AddListener(() => SetPreset(2));
        Preset3.onClick.AddListener(() => SetPreset(3));
        State.LoadRaceData();
        UpdatePresetsVisible();
    }

    public CreateStrategicGame CreateStrategicGame;
    public void CreateStrategic()
    {
        UI.SetActive(false);
        CreateStrategicGame.gameObject.SetActive(true);
        CreateStrategicGame.ClearState();
    }

    public CreateTacticalGame CreateTacticalGame;
    public void CreateTactical()
    {
        UI.SetActive(false);
        CreateTacticalGame.gameObject.SetActive(true);
        CreateTacticalGame.Open();

    }

    private void SetPreset(int value)
    {
        State.ChangeRaceSlotUsed(value);
        UpdatePresetsVisible();
    }

    public void UpdatePresetsVisible()
    {
        if (State.RaceSlot <= 1)
        {
            Preset1.interactable = false;
            Preset2.interactable = true;
            Preset3.interactable = true;
        }
        else if (State.RaceSlot == 2)
        {
            Preset1.interactable = true;
            Preset2.interactable = false;
            Preset3.interactable = true;
        }
        if (State.RaceSlot == 3)
        {
            Preset1.interactable = true;
            Preset2.interactable = true;
            Preset3.interactable = false;
        }
    }

    public void ChangeAssimilableTraits()
    {
        State.AssimilateList.Initialize();
        State.GameManager.VariableEditor.Open(State.AssimilateList);
    }

    public void ChangeRandomizableTraits(int raceInt)
    {
        foreach (var entry in State.RandomizeLists)
        {
            entry.Value.Uninitialize();
        }
        State.RandomizeLists[raceInt].Initialize();
        State.GameManager.VariableEditor.Open(State.RandomizeLists[raceInt]);
    }

    public void ReturnToStart()
    {
        State.TutorialMode = false;
        State.GameManager.TutorialScript = null;
        UI.SetActive(true);
        CreateStrategicGame.gameObject.SetActive(false);
        CreateTacticalGame.gameObject.SetActive(false);
        State.GameManager.Menu.ContentSettings.Refresh();
    }

    public void TutorialMode()
    {
        State.GameManager.TutorialScript = new TutorialScript();
        State.Load($"{Application.streamingAssetsPath}{System.IO.Path.DirectorySeparatorChar}Tutorial.sav", tutorial: true);
    }

    public void LoadGame()
    {
        State.GameManager.OpenSaveLoadMenu();
    }

    public void MapEditor()
    {
        State.World = new World(true);
        State.GameManager.SwitchToMapEditor();
    }

    public override void ReceiveInput()
    {
        //Vector2 currentMousePos = GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
    }

    public override void CleanUp()
    {
    }

}
