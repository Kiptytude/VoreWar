using OdinSerializer;


class BountyGoods
{
    [OdinSerialize]
    internal int Gold;

    public BountyGoods(int gold)
    {
        Gold = gold;
    }

    internal string ApplyToArmyOrVillage(Army army, Village village = null)
    {
        string ret = "";
        if (army != null)
        {
            if (Gold > 0)
            {
                ret = $"\n<color=yellow>Recieved {Gold} Gold</color>\n";
                army.Empire.AddGold(Gold);
                Gold = 0;
            }
        }
        else if (village != null)
        {
            var empire = State.World.GetEmpireOfSide(village.Side);
            if (empire != null)
            {
                ret = $"\n<color=yellow>Recieved {Gold} Gold</color>\n";
                empire.AddGold(Gold);
                Gold = 0;
            }
        }
        return ret;
    }
}

