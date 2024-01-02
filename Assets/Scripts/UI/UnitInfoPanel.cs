using System;
using System.Linq;
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
    static HirePanel UnitPickerUI;

    private int nameInstances;

    public string HoveringName;

    private void Update()
    {
        if (hovering == false)
            return;

        if (Unit == null)
            return;
        TextMeshProUGUI HoverBox;

        if (Input.mousePosition.y > InfoText.transform.parent.position.y && BasicInfo)
            HoverBox = BasicInfo;
        else
            HoverBox = InfoText;

        if (StatBlock)
        {
            // Actually, I changed my mind. I love coding.
            if (IsMousingOver(StatBlock))
            {
                GameObject STRLabel = StatBlock.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
                GameObject DEXLabel = StatBlock.transform.GetChild(0).GetChild(1).GetChild(0).gameObject;
                GameObject MNDLabel = StatBlock.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
                GameObject WLLLabel = StatBlock.transform.GetChild(1).GetChild(1).GetChild(0).gameObject;
                GameObject ENDLabel = StatBlock.transform.GetChild(2).GetChild(0).GetChild(0).gameObject;
                GameObject AGILabel = StatBlock.transform.GetChild(2).GetChild(1).GetChild(0).gameObject;
                GameObject VORLabel = StatBlock.transform.GetChild(3).GetChild(0).GetChild(0).gameObject;
                GameObject STMLabel = StatBlock.transform.GetChild(3).GetChild(1).GetChild(0).gameObject;
                GameObject LDRLabel = StatBlock.transform.GetChild(4).GetChild(0).GetChild(0).gameObject;
                GameObject E1Label = StatBlock.transform.GetChild(5).GetChild(0).GetChild(0).gameObject;
                GameObject E2Label = StatBlock.transform.GetChild(5).GetChild(1).GetChild(0).gameObject;
                GameObject E3Label = StatBlock.transform.GetChild(5).GetChild(2).GetChild(0).gameObject;

                foreach (var item in new GameObject[] {STRLabel, DEXLabel, MNDLabel, WLLLabel, ENDLabel, AGILabel, VORLabel, STMLabel, LDRLabel, E1Label, E2Label, E3Label})
                {
                    if (IsMousingOver(item))
                        HoverBox = item.GetComponent<TextMeshProUGUI>();
                }
            }
        }



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
                if (words[2] == "AssumeShape")
                {
                    if (Actor == null)
                        ShapeshifterPanel(Unit);
                    return;
                }
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

    internal static void RenderShape(Unit selectedUnit, Unit shape)
    {
        GameObject obj = GameObject.Instantiate(UnitPickerUI.ShapesPanel, UnitPickerUI.ActorFolder);
        UIUnitSprite sprite = obj.GetComponentInChildren<UIUnitSprite>();
        Actor_Unit actor = new Actor_Unit(new Vec2i(0, 0), shape);
        //Text text = obj.transform.GetChild(3).GetComponent<Text>();
        Text GenderText = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();
        Text EXPText = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>();
        GameObject EquipRow = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(2).gameObject;
        GameObject StatRow1 = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(3).gameObject;
        GameObject StatRow2 = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(4).gameObject;
        GameObject StatRow3 = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(5).gameObject;
        GameObject StatRow4 = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(6).gameObject;
        Text TraitList = obj.transform.GetChild(2).GetChild(0).GetChild(1).GetChild(0).GetChild(0).gameObject.GetComponent<Text>();

        string gender;
        if (actor.Unit.GetGender() != Gender.None)
        {
            if (actor.Unit.GetGender() == Gender.Hermaphrodite)
                gender = "Herm";
            else
                gender = actor.Unit.GetGender().ToString();
            GenderText.text = $"{gender}";
        }
        EXPText.text = $"Level {shape.Level} ({(int)shape.Experience} EXP)";
        if (actor.Unit.HasTrait(Traits.Resourceful))
        {
            EquipRow.transform.GetChild(2).gameObject.SetActive(true);
            EquipRow.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = shape.GetItem(0)?.Name;
            EquipRow.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = shape.GetItem(1)?.Name;
            EquipRow.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = shape.GetItem(2)?.Name;
        }
        else
        {
            EquipRow.transform.GetChild(2).gameObject.SetActive(false);
            EquipRow.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = shape.GetItem(0)?.Name;
            EquipRow.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = shape.GetItem(1)?.Name;
        }
        StatRow1.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = shape.GetStatBase(Stat.Strength).ToString();
        StatRow1.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = shape.GetStatBase(Stat.Dexterity).ToString();
        StatRow2.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = shape.GetStatBase(Stat.Mind).ToString();
        StatRow2.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = shape.GetStatBase(Stat.Will).ToString();
        StatRow3.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = shape.GetStatBase(Stat.Endurance).ToString();
        StatRow3.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = shape.GetStatBase(Stat.Agility).ToString();
        if (actor.PredatorComponent != null)
        {
            StatRow4.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = shape.GetStatBase(Stat.Voracity).ToString();
            StatRow4.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = shape.GetStatBase(Stat.Stomach).ToString();
        }
        else
            StatRow4.SetActive(false);
        TraitList.text = RaceEditorPanel.TraitListToText(shape.GetTraits, true).Replace(", ", "\n");
        //text.text += $"STR: {unit.GetStatBase(Stat.Strength)} DEX: { unit.GetStatBase(Stat.Dexterity)}\n" +
        //    $"MND: {unit.GetStatBase(Stat.Mind)} WLL: { unit.GetStatBase(Stat.Will)} \n" +
        //    $"END: {unit.GetStatBase(Stat.Endurance)} AGI: {unit.GetStatBase(Stat.Agility)}\n";
        //if (actor.PredatorComponent != null)
        //    text.text += $"VOR: {unit.GetStatBase(Stat.Voracity)} STM: { unit.GetStatBase(Stat.Stomach)}";
        actor.UpdateBestWeapons();
        sprite.UpdateSprites(actor);
        sprite.Name.text = shape.Name;
        Button[] buttons = obj.GetComponentsInChildren<Button>();
        buttons[0].GetComponentInChildren<Text>().text = "Transform";
        buttons[0].onClick.AddListener(() => Shapeshift(selectedUnit, shape));
        buttons[0].onClick.AddListener(() => obj.SetActive(false));
        buttons[1].GetComponentInChildren<Text>().text = "Discard";
        buttons[1].onClick.AddListener(() => selectedUnit.ShifterShapes.Remove(shape));
        buttons[1].onClick.AddListener(() => obj.SetActive(false));
    }

    internal static void Shapeshift(Unit selectedUnit, Unit shape)
    {
        selectedUnit.ShifterShapes.Remove(shape);
        
        shape.ShifterShapes = selectedUnit.ShifterShapes;
        if(!shape.ShifterShapes.Contains(selectedUnit)) 
            shape.ShifterShapes.Add(selectedUnit);
        shape.Side = selectedUnit.Side;
        shape.Health = shape.MaxHealth;

        RenderShape(shape,selectedUnit);
        State.GameManager.Recruit_Mode.army.Units.Add(shape);
        State.GameManager.Recruit_Mode.army.Units.Remove(selectedUnit);
        State.GameManager.Recruit_Mode.UpdateActorList();
        //State.GameManager.Recruit_Mode.ButtonCallback(10);
    }

    internal static void ShapeshifterPanel(Unit selectedUnit)
    {
        selectedUnit.UpdateShapeExpAndItems(false);
        if (UnitPickerUI == null)
            UnitPickerUI = State.GameManager.Recruit_Mode.HireUI;
        int children = UnitPickerUI.ActorFolder.transform.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            UnityEngine.Object.Destroy(UnitPickerUI.ActorFolder.transform.GetChild(i).gameObject);
        }
        foreach (Unit shape in selectedUnit.ShifterShapes)
        {
            RenderShape(selectedUnit,shape);
        }
        UnitPickerUI.ActorFolder.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 300 * (1 + (children) / 3));
        UnitPickerUI.GetComponentsInChildren<Button>().Last().GetComponentInChildren<Text>().text = "Cancel";
        UnitPickerUI.gameObject.SetActive(true);
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

    public bool IsMousingOver(GameObject thing)
    {
        Vector3[] v = new Vector3[4];
        thing.GetComponent<RectTransform>().GetWorldCorners(v);
        Rect rect = new Rect(v[0].x, v[0].y, v[2].x - v[0].x, v[2].y - v[0].y);
        return rect.Contains(Input.mousePosition);
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
