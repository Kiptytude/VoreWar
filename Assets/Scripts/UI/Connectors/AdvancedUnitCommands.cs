using System;
using UnityEngine;
using UnityEngine.UI;

public class AdvancedUnitCommands : MonoBehaviour
{
    public GameObject ButtonPrefab;
    internal Button[] Buttons;

    int index = 0;

    internal void ClearButtons()
    {
        if (Buttons == null)
            Buttons = new Button[25];
        for (int i = 0; i < Buttons.Length; i++)
        {
            if (Buttons[i] != null)
            {
                Buttons[i].gameObject.SetActive(false);
            }
        }
    }

    internal void SetUpButtons(Actor_Unit actor)
    {

        ClearButtons();
        index = 0;

        foreach (var action in TacticalActionList.TargetedActions)
        {
            if (action.AppearConditional(actor) && (action.RequiresPred == false || actor.Unit.Predator))
            {
                if (action.Name == "Tail Vore" && actor.Unit.Race == Race.Terrorbird)
                    SetButton("Crop Vore", action.OnClicked, action.ButtonColor);
                else
                    SetButton(action.Name, action.OnClicked, action.ButtonColor);
            }
        }

        foreach (var action in TacticalActionList.UntargetedActions)
        {
            if (action.AppearConditional(actor))
                SetButton(action.Name, action.OnClicked, action.ButtonColor, false);
        }

        foreach (var spell in actor.Unit.UseableSpells)
        {
            SetButtonSpell(actor, spell);
        }

        int maxSize = Math.Min(800 / index, 60);
        for (int i = 0; i < Buttons.Length; i++)
        {
            if (Buttons[i] != null)
            {
                Buttons[i].GetComponent<RectTransform>().sizeDelta = new Vector2(160, maxSize);
            }
        }

    }

    internal Button SetButton(string text, Action action, Color color, bool marksSelected = true)
    {
        Button button;
        if (Buttons[index] == null)
        {
            button = Instantiate(ButtonPrefab, transform).GetComponent<Button>();
            var trans = button.GetComponent<RectTransform>();
            trans.sizeDelta = new Vector2(160, 60);
            Buttons[index] = button;
        }
        else
        {
            button = Buttons[index];
            button.onClick.RemoveAllListeners();
        }
        ColorBlock cb = button.colors;
        cb.normalColor = color;
        cb.highlightedColor = color * 1.2f;
        Color pressed = color * .7f;
        pressed.a = 1;
        cb.pressedColor = pressed;
        button.colors = cb;

        button.GetComponentInChildren<Text>().text = text;
        button.onClick.AddListener(new UnityEngine.Events.UnityAction(action));
        if (marksSelected)
        {
            button.onClick.AddListener(() =>
            {
                if (State.GameManager.TacticalMode.ActionMode == 4)
                    State.GameManager.TacticalMode.CommandsUI.SelectorIcon.transform.position = button.transform.position;
            });
        }

        button.gameObject.SetActive(true);
        button.interactable = true;
        index++;
        return button;
    }

    internal Button SetButtonSpell(Actor_Unit actor, Spell spell)
    {
        Color color = new Color(.669f, .753f, 1);
        Button button;
        if (Buttons[index] == null)
        {
            button = Instantiate(ButtonPrefab, transform).GetComponent<Button>();
            var trans = button.GetComponent<RectTransform>();
            trans.sizeDelta = new Vector2(160, 60);
            Buttons[index] = button;
        }
        else
        {
            button = Buttons[index];
            button.onClick.RemoveAllListeners();
        }
        button.GetComponentInChildren<Text>().text = spell.Name + (actor.Unit.Mana >= spell.ManaCost ? "" : "\n(no mana)");
        button.onClick.AddListener(new UnityEngine.Events.UnityAction(() => State.GameManager.TacticalMode.SetMagicMode(spell)));

        button.onClick.AddListener(() =>
        {
            if (State.GameManager.TacticalMode.ActionMode == 6)
                State.GameManager.TacticalMode.CommandsUI.SelectorIcon.transform.position = button.transform.position;
        });

        ColorBlock cb = button.colors;
        cb.normalColor = color;
        cb.highlightedColor = color * 1.2f;
        Color pressed = color * .7f;
        pressed.a = 1;
        cb.pressedColor = pressed;
        button.colors = cb;

        button.interactable = actor.Unit.Mana >= spell.ManaCost;
        button.gameObject.SetActive(true);
        index++;
        return button;
    }

    public GameObject SelectorIcon;
}
