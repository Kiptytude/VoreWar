using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitInfoPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool hovering;

    int prevNames = 0;

    public UIUnitSprite Sprite;
    public TextMeshProUGUI BasicInfo;
    public TextMeshProUGUI InfoText;
    public GameObject StatBlock;
    public Slider ExpBar;
    public Slider HealthBar;
    public Slider ManaBar;
    internal Unit Unit;
    internal Actor_Unit Actor;

    private int nameInstances;

    public string HoveringName;

    private void Update()
    {
        if (hovering == false)
            return;

        if (Unit == null)
            return;
        TextMeshProUGUI HoverBox;
        if (Input.mousePosition.y > InfoText.transform.parent.position.y)
            HoverBox = BasicInfo;
        else
            HoverBox = InfoText;
        int wordIndex = TMP_TextUtilities.FindIntersectingWord(HoverBox, Input.mousePosition, null);
        //if (wordIndex <= -1 && BasicInfo)
        //{
        //    wordIndex = TMP_TextUtilities.FindIntersectingWord(BasicInfo, Input.mousePosition, null);
        //    HoverBox = BasicInfo;
        //}
        if (wordIndex > -1)
        {
            string[] words = new string[5];
            for (int i = 0; i < 5; i++)
            {
                if (wordIndex - 2 + i < 0 || wordIndex - 2 + i >= HoverBox.textInfo.wordCount || HoverBox.textInfo.wordInfo[wordIndex - 2 + i].characterCount < 1)
                {
                    words[i] = string.Empty;
                    continue;
                }
                words[i] = HoverBox.textInfo.wordInfo[wordIndex - 2 + i].GetWord();
            }

            State.GameManager.HoveringTooltip.UpdateInformation(words, Unit, Actor);
            if (Input.GetMouseButtonDown(0))
            {
                if (words[2] == "UnitEditor")
                {
                    if (Actor == null)
                        State.GameManager.UnitEditor.Open(Unit);
                    else
                        State.GameManager.UnitEditor.Open(Actor);
                    return;
                }
                if (nameInstances <= 1)
                    DisplayInfoFor(HoveringName);
                else
                {
                    prevNames = 0;
                    for (int i = 2; i < wordIndex; i++) //Don't use your own name as the first name
                    {
                        if (HoverBox.textInfo.wordInfo[i].GetWord() == words[2])
                            prevNames += 1;
                        else if (wordIndex + 1 >= HoverBox.textInfo.wordCount && HoveringName == $"{HoverBox.textInfo.wordInfo[i].GetWord()} {HoverBox.textInfo.wordInfo[i + 1].GetWord()}")
                            prevNames += 1;
                        else if (wordIndex + 2 >= HoverBox.textInfo.wordCount && HoveringName == $"{HoverBox.textInfo.wordInfo[i].GetWord()} {HoverBox.textInfo.wordInfo[i + 1].GetWord()} {HoverBox.textInfo.wordInfo[i + 2].GetWord()}")
                            prevNames += 1;
                    }
                    DisplayInfoFor(HoveringName, prevNames);
                }
            }
            else
            {
                nameInstances = 0;
                if (Actor?.Unit != null && (bool)Actor?.Unit.Predator)
                {
                    foreach (var prey in Actor.PredatorComponent.GetAllPrey())
                    {
                        if (prey.Unit.Name == words[2])
                        {
                            State.GameManager.HoveringTooltip.HoveringValidName();
                            HoveringName = words[2];
                            nameInstances += 1;
                        }
                        else if (prey.Unit.Name == $"{words[2]} {words[3]}")
                        {
                            State.GameManager.HoveringTooltip.HoveringValidName();
                            HoveringName = $"{words[2]} {words[3]}";
                            nameInstances += 1;
                        }
                        else if (prey.Unit.Name == $"{words[2]} {words[3]} {words[4]}")
                        {
                            State.GameManager.HoveringTooltip.HoveringValidName();
                            HoveringName = $"{words[2]} {words[3]} {words[4]}";
                            nameInstances += 1;
                        }
                    }
                }
                if (nameInstances == 0)
                    HoveringName = "RAREASFARARA";
            }
        }
    }

    void DisplayInfoFor(string name)
    {
        if (Actor?.Unit.Predator == false || Actor?.PredatorComponent == null)
            return;
        foreach (var prey in Actor.PredatorComponent.GetAllPrey())
        {
            if (prey.Unit.Name == name)
            {
                State.GameManager.TacticalMode.InfoPanel.RefreshTacticalUnitInfo(prey.Actor);
            }
        }
    }

    void DisplayInfoFor(string name, int instance)
    {
        int count = 0;
        if (Actor?.Unit.Predator == false)
            return;
        foreach (var prey in Actor.PredatorComponent.GetAllPrey())
        {
            if (prey.Unit.Name == name)
            {

                if (count == prevNames)
                {
                    State.GameManager.TacticalMode.InfoPanel.RefreshTacticalUnitInfo(prey.Actor);
                    break;
                }
                else
                    count++;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovering = false;
    }
}
