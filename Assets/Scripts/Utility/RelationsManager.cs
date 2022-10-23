using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

static class RelationsManager
{
    /// <summary>
    /// Creates the network of relations from scratch (leaves monsters to be auto-created)
    /// </summary>
    static internal void ResetRelations()
    {
        var sides = State.World.MainEmpires.Select(s => s.Side).ToList();
        sides.Add((int)Race.Goblins);
        State.World.Relations = new Dictionary<int, Dictionary<int, Relationship>>();
        foreach (int side in sides)
        {
            State.World.Relations[side] = new Dictionary<int, Relationship>();
            foreach (int targetSide in sides)
            {
                State.World.Relations[side][targetSide] = new Relationship(State.World.GetEmpireOfSide(side)?.Team ?? -1, State.World.GetEmpireOfSide(targetSide)?.Team ?? -1);
            }
        }
    }

    static internal void ResetMonsterRelations()
    {
        var sides = State.World.AllActiveEmpires.Select(s => s.Side).ToList();
        foreach (int side in sides)
        {
            foreach (int targetSide in sides)
            {
                if (side >= 100 || targetSide >= 100)
                {
                    if (State.World.Relations.ContainsKey(side))
                        State.World.Relations[side].Remove(targetSide);
                }
                    
            }
        }
    }

    /// <summary>
    /// Resets the type of relation to be based on the teams, but doesn't change the actual relations values
    /// </summary>
    static internal void ResetRelationTypes()
    {
        var sides = State.World.MainEmpires.Select(s => s.Side).ToList();
        foreach (int side in sides)
        {
            foreach (int targetSide in sides)
            {
                RelationState newType = State.World.GetEmpireOfSide(side).Team == State.World.GetEmpireOfSide(targetSide).Team ? RelationState.Allied : RelationState.Enemies;
                GetRelation(side, targetSide).Type = newType;
            }
        }
    }

    static internal void TeamUpdated(Empire empire)
    {
        foreach (Empire otherEmp in State.World.AllActiveEmpires)
        {
            var rel = GetRelation(empire.Side, otherEmp.Side);
            var counterRel = GetRelation(otherEmp.Side, empire.Side);
            if (rel.Type == RelationState.Allied && otherEmp.Team != empire.Team)
            {
                rel.Type = RelationState.Enemies;
                rel.Attitude = -1;
                counterRel.Type = RelationState.Enemies;
                counterRel.Attitude = -1;
            }
            if ((rel.Type == RelationState.Enemies || rel.Type == RelationState.Neutral) && otherEmp.Team == empire.Team)
            {
                rel.Type = RelationState.Allied;
                rel.Attitude = 2;
                counterRel.Type = RelationState.Allied;
                counterRel.Attitude = 2;
            }

        }
    }

    static internal Relationship GetRelation(int sideA, int sideB)
    {
        if (State.World.Relations == null)
            ResetRelations();
        if (sideA == 700 || sideA == 701 || sideB == 700 || sideB == 701)
        {
            if (sideA == sideB)
                return new Relationship(0, 0);
            else
                return new Relationship(-1, -1);
        }
        else if (sideA >= 100 || sideB >= 100)
        {

        }
        if (State.World.Relations.TryGetValue(sideA, out var dict))
        {
            if (dict.TryGetValue(sideB, out var rel))
            {
                return rel;
            }
            var empAI = State.World.GetEmpireOfSide(sideA);
            var empBI = State.World.GetEmpireOfSide(sideB);
            if (empAI == null || empBI == null)
            {
                Debug.Log($"Invalid relationship returned between {sideA} and {sideB}");
                return new Relationship(0, 1);
            }

            Relationship newRel = new Relationship(empAI.Team, empBI.Team);
            dict[sideB] = newRel;
            return newRel;
        }
        var empA = State.World.GetEmpireOfSide(sideA);
        var empB = State.World.GetEmpireOfSide(sideB);
        if (empA == null || empB == null)
        {
            Debug.Log($"Invalid relationship returned between {sideA} and {sideB}");
            return new Relationship(0, 1);
        }
        var newDict = new Dictionary<int, Relationship>();
        State.World.Relations[sideA] = newDict;
        Relationship newRel2 = new Relationship(empA.Team, empB.Team);
        newDict[sideB] = newRel2;
        return newRel2;

    }

    struct RelCata
    {
        internal float Goal;
        internal float IncreaseMult;
        internal float IncreaseAdd;
        internal float DecreaseMult;
        internal float DecreaseAdd;

        public RelCata(float goal, float increaseMult, float increaseAdd, float decreaseMult, float decreaseAdd)
        {
            Goal = goal;
            IncreaseMult = increaseMult;
            IncreaseAdd = increaseAdd;
            DecreaseMult = decreaseMult;
            DecreaseAdd = decreaseAdd;
        }
    }

    static internal void TurnElapsed()
    {
        RelCata Neutral;
        RelCata Allied;
        RelCata Enemies;
        switch (Config.DiplomacyScale)
        {
            case DiplomacyScale.Default:
                Neutral = new RelCata(0, .02f, .02f, .002f, .004f);
                Allied = new RelCata(2, .02f, .01f, .002f, .003f);
                Enemies = new RelCata(-.25f, .01f, .01f, .01f, .005f);
                break;
            case DiplomacyScale.Suspicious:
                Neutral = new RelCata(-0.2f, .02f, .02f, .005f, .0075f);
                Allied = new RelCata(1.5f, .02f, .01f, .005f, .0055f);
                Enemies = new RelCata(-2f, .01f, .01f, .02f, .01f);
                break;
            case DiplomacyScale.Distrustful:
                Neutral = new RelCata(-0.4f, .02f, .02f, .02f, .02f);
                Allied = new RelCata(0.8f, .02f, .01f, .01f, .015f);
                Enemies = new RelCata(-3f, .01f, .01f, .03f, .03f);
                break;
            case DiplomacyScale.Friendly:
                Neutral = new RelCata(0.5f, .04f, .04f, .002f, .004f);
                Allied = new RelCata(3f, .06f, .06f, .002f, .003f);
                Enemies = new RelCata(0, .04f, .04f, .01f, .002f);
                break;
            default: //No scaling
                return;
        }
        foreach (var list in State.World.Relations.Values)
        {
            foreach (var rel in list.Values)
            {
                switch (rel.Type)
                {
                    case RelationState.Neutral:
                        Update(rel, Neutral);
                        break;
                    case RelationState.Allied:
                        Update(rel, Allied);
                        break;
                    case RelationState.Enemies:
                        Update(rel, Enemies);
                        break;
                }

            }
        }

        void Update(Relationship rel, RelCata cata)
        {
            if (rel.TurnsSinceAsked >= 0)
                rel.TurnsSinceAsked++;
            if (rel.Attitude < cata.Goal)
            {
                rel.Attitude = Mathf.Lerp(rel.Attitude, cata.Goal, cata.IncreaseMult);
                rel.Attitude += cata.IncreaseAdd;
            }

            if (rel.Attitude > cata.Goal)
            {
                rel.Attitude = Mathf.Lerp(rel.Attitude, cata.Goal, cata.DecreaseMult);
                rel.Attitude -= cata.DecreaseAdd;

            }
        }
    }

    static internal void SetWar(Empire sideA, Empire sideB)
    {
        var relation = GetRelation(sideA.Side, sideB.Side);
        relation.Type = RelationState.Enemies;
        var counterRelation = GetRelation(sideB.Side, sideA.Side);
        counterRelation.Type = RelationState.Enemies;
        relation.TurnsSinceAsked = -1;
        counterRelation.TurnsSinceAsked = -1;
    }

    static internal void SetPeace(Empire sideA, Empire sideB)
    {
        var relation = GetRelation(sideA.Side, sideB.Side);
        relation.Type = RelationState.Neutral;
        var counterRelation = GetRelation(sideB.Side, sideA.Side);
        counterRelation.Type = RelationState.Neutral;
        relation.TurnsSinceAsked = -1;
        counterRelation.TurnsSinceAsked = -1;
    }

    static internal void SetAlly(Empire sideA, Empire sideB)
    {
        var relation = GetRelation(sideA.Side, sideB.Side);
        relation.Type = RelationState.Allied;
        var counterRelation = GetRelation(sideB.Side, sideA.Side);
        counterRelation.Type = RelationState.Allied;
        relation.TurnsSinceAsked = -1;
        counterRelation.TurnsSinceAsked = -1;
    }

    static internal void VillageAttacked(Empire attacker, Empire defender)
    {
        if (attacker is MonsterEmpire || defender is MonsterEmpire)
            return;
        if (GetRelation(defender.Side, attacker.Side).Attitude > -.25f)
            GetRelation(defender.Side, attacker.Side).Attitude = -.25f;
        GetRelation(defender.Side, attacker.Side).Attitude -= .3f;
        foreach (Empire emp in State.World.MainEmpires)
        {
            if (emp == attacker || emp == defender)
                continue;
            var attackerRel = GetRelation(emp.Side, attacker.Side);
            var defenderRel = GetRelation(emp.Side, defender.Side);
            if (defenderRel.Type == RelationState.Allied)
            {
                attackerRel.Attitude -= .15f;
            }
            else if (defenderRel.Type == RelationState.Enemies)
            {
                attackerRel.Attitude += .1f;
            }
        }
    }

    static internal void GoldMineTaken(Empire attacker, Empire defender)
    {
        if (attacker is MonsterEmpire || defender is MonsterEmpire)
            return;
        if (defender == null)
            return;
        if (GetRelation(defender.Side, attacker.Side).Attitude > 0)
            GetRelation(defender.Side, attacker.Side).Attitude *= .75f;
        GetRelation(defender.Side, attacker.Side).Attitude -= .1f;
        foreach (Empire emp in State.World.MainEmpires)
        {
            if (emp == attacker || emp == defender)
                continue;
            var attackerRel = GetRelation(emp.Side, attacker.Side);
            var defenderRel = GetRelation(emp.Side, defender.Side);
            if (defenderRel.Type == RelationState.Allied)
            {
                attackerRel.Attitude -= .05f;
            }
            else if (defenderRel.Type == RelationState.Enemies)
            {
                attackerRel.Attitude += .0375f;
            }
        }
    }

    static internal void ArmyAttacked(Empire attacker, Empire defender)
    {
        if (attacker is MonsterEmpire || defender is MonsterEmpire)
            return;
        GetRelation(defender.Side, attacker.Side).Attitude -= .2f;
        foreach (Empire emp in State.World.MainEmpires)
        {
            if (emp == attacker || emp == defender)
                continue;
            var attackerRel = GetRelation(emp.Side, attacker.Side);
            var defenderRel = GetRelation(emp.Side, defender.Side);
            if (defenderRel.Type == RelationState.Allied)
            {
                attackerRel.Attitude -= .1f;
            }
            else if (defenderRel.Type == RelationState.Enemies)
            {
                attackerRel.Attitude += .075f;
            }
        }
    }

    static internal void CityReturned(Empire giver, Empire receiver)
    {
        if (giver == null || receiver == null)
            return;
        if (giver is MonsterEmpire || receiver is MonsterEmpire)
            return;
        GetRelation(receiver.Side, giver.Side).Attitude += 1f;
        foreach (Empire emp in State.World.MainEmpires)
        {
            if (emp == giver || emp == receiver)
                continue;
            var giverRel = GetRelation(emp.Side, giver.Side);
            var receiverRel = GetRelation(emp.Side, receiver.Side);
            if (receiverRel.Type == RelationState.Allied)
            {
                giverRel.Attitude += .2f;
            }
        }
    }

    static internal void ChangeRelations(Empire likee, Empire liker, float increase)
    {
        var relation = GetRelation(liker.Side, likee.Side);
        relation.Attitude += increase;
    }

    static internal void MakeHate(Empire attacker, Empire defender)
    {
        var relation = GetRelation(defender.Side, attacker.Side);
        if (relation.Attitude > -1.5f)
            relation.Attitude = -1.5f;
        SetWar(attacker, defender);
    }

    static internal void MakeLike(Empire likee, Empire liker, float setMinRelation = 1)
    {
        var relation = GetRelation(liker.Side, likee.Side);
        if (relation.Attitude < setMinRelation)
            relation.Attitude = setMinRelation;
        SetAlly(likee, liker);
    }

    static internal void Genocide(Empire attacker, Empire defender)
    {
        if (attacker is MonsterEmpire || defender is MonsterEmpire)
            return;
        GetRelation(defender.Side, attacker.Side).Attitude -= .6f;
        foreach (Empire emp in State.World.MainEmpires)
        {
            if (emp == attacker || emp == defender)
                continue;
            var attackerRel = GetRelation(emp.Side, attacker.Side);
            var defenderRel = GetRelation(emp.Side, defender.Side);
            if (defenderRel.Type == RelationState.Allied)
            {
                attackerRel.Attitude -= .4f;
            }
            else if (defenderRel.Type == RelationState.Enemies)
            {
                attackerRel.Attitude += .1f;
            }
        }
    }

    static internal void AskPlayerForPeace(Empire AI, Empire player)
    {
        var box = State.GameManager.CreateDialogBox();
        State.GameManager.ActiveInput = true;
        box.SetData(() => { SetPeace(AI, player); State.GameManager.ActiveInput = false; }, "Accept", "Reject", $"The {AI.Name} wants to know if you ({player.Name}) would accept a peace treaty?", () => { GetRelation(AI.Side, player.Side).TurnsSinceAsked = 0; State.GameManager.ActiveInput = false; });
    }

    static internal void AskPlayerForAlliance(Empire AI, Empire player)
    {
        var box = State.GameManager.CreateDialogBox();
        State.GameManager.ActiveInput = true;
        box.SetData(() => { SetAlly(AI, player); State.GameManager.ActiveInput = false; }, "Accept", "Reject", $"The {AI.Name} wants to know if you ({player.Name}) would accept an alliance?", () => { GetRelation(AI.Side, player.Side).TurnsSinceAsked = 0; State.GameManager.ActiveInput = false; });
    }

}

