using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiplomacyScreen : MonoBehaviour
{


    public UIUnitSprite Leader;

    public TextMeshProUGUI Information;
    public TextMeshProUGUI Updates;

    public Button AskPeaceButton;
    public Button AskAllyButton;
    public Button DeclareWarButton;


    public TMP_InputField GoldAmount;

    internal Empire Player;
    internal Empire Other;

    public void Open(Empire player, Empire other)
    {
        if (other == null)
            return;
        Unit unit = other.Leader;
        if (unit == null)
        {
            unit = new Unit(other.Side, other.Race, 0, true);
        }
        Actor_Unit actor = new Actor_Unit(new Vec2i(0, 0), unit);
        actor.UpdateBestWeapons();
        Leader.UpdateSprites(actor);
        Player = player;
        Other = other;
        gameObject.SetActive(true);
        Updates.text = "";
        UpdateMainText();

    }

    private void UpdateMainText()
    {

        var relation = RelationsManager.GetRelation(Other.Side, Player.Side);
        var counterRelation = RelationsManager.GetRelation(Player.Side, Other.Side);
        Information.text = $"<b>{Other.Name}</b>\nTheir opinion of us: {Math.Round(relation.Attitude, 3)}\nOur opinion of them: {Math.Round(counterRelation.Attitude, 3)}\nType: {relation.Type}";
        if (Config.Diplomacy == false)
        {
            Updates.text = "Diplomacy is turned off, you can only negotiate with the goblins";
            DeclareWarButton.gameObject.SetActive(Other.Race == Race.Goblins);
            AskAllyButton.gameObject.SetActive(Other.Race == Race.Goblins);
            AskPeaceButton.gameObject.SetActive(Other.Race == Race.Goblins);
        }
        else
        {
            DeclareWarButton.gameObject.SetActive(true);
            AskAllyButton.gameObject.SetActive(true);
            AskPeaceButton.gameObject.SetActive(true);
        }
        if (Config.Diplomacy == false && Other.Race != Race.Goblins)
        {
            DeclareWarButton.interactable = false;
            AskAllyButton.interactable = false;
            AskPeaceButton.interactable = false;
        }
        else if (relation.Type == RelationState.Allied)
        {
            DeclareWarButton.interactable = true;
            AskAllyButton.interactable = false;
            AskPeaceButton.interactable = false;
        }
        else if (relation.Type == RelationState.Neutral)
        {
            DeclareWarButton.interactable = true;
            AskAllyButton.interactable = true;
            AskPeaceButton.interactable = false;
        }
        else if (relation.Type == RelationState.Enemies)
        {
            DeclareWarButton.interactable = false;
            AskAllyButton.interactable = false;
            AskPeaceButton.interactable = true;
        }

    }

    public void AskPeace()
    {
        var relation = RelationsManager.GetRelation(Other.Side, Player.Side);
        if (relation.Type != RelationState.Enemies)
            return;
        if (relation.Attitude >= -.25f)
        {
            RelationsManager.SetPeace(Player, Other);
            UpdateMainText();
            Updates.text = "Peace was accepted";
        }
        else
            Updates.text = "They don't like you enough to accept peace";
    }

    public void DeclareWar()
    {
        var relation = RelationsManager.GetRelation(Other.Side, Player.Side);
        if (relation.Type != RelationState.Enemies)
        {
            if (relation.Attitude > 0)
                relation.Attitude *= .6f;
            RelationsManager.SetWar(Player, Other);
            UpdateMainText();
            Updates.text = "War was declared";
        }
    }

    public void AskAllies()
    {
        var relation = RelationsManager.GetRelation(Other.Side, Player.Side);
        if (relation.Type != RelationState.Neutral)
            return;
        if (relation.Attitude >= .75f)
        {
            RelationsManager.SetAlly(Player, Other);
            UpdateMainText();
            Updates.text = $"You are now allies of the {Other.Name}";
        }
        else
            Updates.text = "They don't like you enough to accept being allies";
    }

    public void GiveGold()
    {
        var relation = RelationsManager.GetRelation(Other.Side, Player.Side);
        if (int.TryParse(GoldAmount.text, out int amount))
        {
            if (amount <= 0)
            {
                Updates.text = $"Have to give positive gold";
            }
            else if (Player.Gold >= amount)
            {
                Player.SpendGold(amount);
                Other.AddGold(amount);
                relation.Attitude += amount / 5000f;
                UpdateMainText();
                Updates.text = $"Gave them {amount} gold";
                State.GameManager.StrategyMode.Regenerate();
            }
            else
            {
                Updates.text = $"Didn't have enough gold to give {amount}, you only have {State.World.ActingEmpire.Gold}";
            }
        }

    }


}
