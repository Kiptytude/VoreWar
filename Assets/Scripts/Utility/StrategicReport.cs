using OdinSerializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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

