using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HelpPanel : MonoBehaviour
{
    bool generatedHelp;
    public Transform ButtonFolder;
    public GameObject[] HelpPages;
    public GameObject ButtonPrefab;

    public TextMeshProUGUI Text;

    public void GenerateButtonsIfNeeded()
    {
        if (generatedHelp)
            return;
        CreatePageButton(HelpText.GameBasics, "Game Basics");
        CreatePageButton(HelpText.StrategyMode, "Strategy Mode");
        CreatePageButton(HelpText.RecruitMode, "Recruit Screen");
        CreatePageButton(HelpText.TacticalMode, "Tactical Mode");
        CreatePageButton(HelpText.Stats, "Character Stats");
        CreatePageButton(HelpText.Items, "Items");
        CreatePageButton(HelpText.Vore, "Vore");
        CreatePageButton(HelpText.Experience, "Experience");
        CreatePageButton(HelpText.Controls, "Controls");
        CreatePageButton(HelpText.Teams, "Teams");
        CreatePageButton(HelpText.Diplomacy, "Diplomacy");
        CreatePageButton(HelpText.Traits(), "Traits");
        CreatePageButton(HelpText.Spells(), "Spells");
        Text.text = HelpText.GameBasics;



        Button exitButton = Instantiate(ButtonPrefab, ButtonFolder).GetComponent<Button>();
        exitButton.GetComponentInChildren<Text>().text = "Exit Help";
        exitButton.onClick.AddListener(FindObjectOfType<GameMenu>().CloseHelp);
        generatedHelp = true;

    }

    void CreatePageButton(string pageText, string pageName)
    {
        Button button = Instantiate(ButtonPrefab, ButtonFolder).GetComponent<Button>();
        button.GetComponentInChildren<Text>().text = pageName;
        button.onClick.AddListener(() => Text.text = pageText);
    }
}
