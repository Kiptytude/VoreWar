using OdinSerializer;

class PassiveAI : IStrategicAI
{
    [OdinSerialize]
    int AISide;

    public PassiveAI(int aISide)
    {
        AISide = aISide;
    }

    public bool RunAI()
    {
        return false;
    }

    public bool TurnAI()
    {
        //Empire empire = State.World.Empires[AISide];
        Village[] villages = State.World.Villages;
        for (int i = 0; i < State.World.Villages.Length; i++)
        {
            if (villages[i].Side == AISide)
            {
                StrategicUtilities.BuyBasicWeapons(villages[i]);
            }
        }
        return false;
    }
}

