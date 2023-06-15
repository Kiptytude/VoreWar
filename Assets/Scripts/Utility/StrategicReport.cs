using OdinSerializer;


struct StrategicReport
{
    [OdinSerialize]
    internal string Text;
    [OdinSerialize]
    internal Vec2 Position;

    public StrategicReport(string text, Vec2 position)
    {
        Text = text;
        Position = position;
    }
}

