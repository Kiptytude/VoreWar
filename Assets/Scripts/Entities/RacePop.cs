using OdinSerializer;

public class RacePop
{

    [OdinSerialize]
    public Race Race { get; set; }
    [OdinSerialize]
    public int Population { get; private set; }
    [OdinSerialize]
    public int Hireables { get; set; }

    public RacePop(Race inRace, int popChange, int hireables)
    {
        Race = inRace;
        Population = popChange;
        Hireables = hireables;
    }

    public RacePop(Race inRace, int popChange)
    {
        Race = inRace;
        Population = popChange;
    }


    internal void ChangePop(int popChange)
    {
        Population += popChange;

    }
}