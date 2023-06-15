using OdinSerializer;

class Relationship
{
    [OdinSerialize]
    internal float Attitude = 0;

    [OdinSerialize]
    internal int TurnsSinceAsked = -1;

    [OdinSerialize]
    internal RelationState Type = RelationState.Neutral;

    public Relationship(int firstTeam, int secondTeam)
    {
        if (firstTeam == -1 || secondTeam == -1)
        {
            Type = RelationState.Enemies;
            Attitude = -.75f;
            switch (Config.DiplomacyScale)
            {
                case DiplomacyScale.Default:
                    Attitude = -.75f;
                    break;
                case DiplomacyScale.Suspicious:
                    Attitude = -1.25f;
                    break;
                case DiplomacyScale.Distrustful:
                    Attitude = -5;
                    break;
                case DiplomacyScale.Friendly:
                    Attitude = 0;
                    break;
                case DiplomacyScale.Unforgetting:
                    Attitude = -.75f;
                    break;
            }
            return;
        }
        if (firstTeam == -200 || secondTeam == -200)
        {
            Type = RelationState.Neutral;
            Attitude = 0;
            return;
        }

        if (firstTeam == secondTeam)
        {
            Type = RelationState.Allied;
            Attitude = 3;
            switch (Config.DiplomacyScale)
            {
                case DiplomacyScale.Default:
                    Attitude = 3;
                    break;
                case DiplomacyScale.Suspicious:
                    Attitude = 2;
                    break;
                case DiplomacyScale.Distrustful:
                    Attitude = 1;
                    break;
                case DiplomacyScale.Friendly:
                    Attitude = 5;
                    break;
                case DiplomacyScale.Unforgetting:
                    Attitude = 3f;
                    break;
            }
            return;

        }
        else
        {
            Type = RelationState.Enemies;
            Attitude = -.75f;
            switch (Config.DiplomacyScale)
            {
                case DiplomacyScale.Default:
                    Attitude = -.75f;
                    break;
                case DiplomacyScale.Suspicious:
                    Attitude = -1.25f;
                    break;
                case DiplomacyScale.Distrustful:
                    Attitude = -5;
                    break;
                case DiplomacyScale.Friendly:
                    Attitude = 0;
                    break;
                case DiplomacyScale.Unforgetting:
                    Attitude = -.75f;
                    break;
            }
            return;
        }
    }

}

