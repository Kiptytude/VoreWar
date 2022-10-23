using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


struct CustomEvent
{
    internal string MainText;
    internal string Option1Choice;
    internal string Option1Result;
    internal string Option2Choice;
    internal string Option2Result;
    internal int Option1Gold;
    internal int Option1Population;
    internal int Option1Happiness;
    internal int Option1HappinessAll;
    internal float Option1Relations;
    internal int Option2Gold;
    internal int Option2Population;
    internal int Option2Happiness;
    internal int Option2HappinessAll;
    internal float Option2Relations;


}

class CustomEventList
{
    List<CustomEvent> Events;

    internal bool AnyEvents => Events.Any();

    internal CustomEvent GetRandom()
    {
        return Events[State.Rand.Next(Events.Count)];
    }

    internal int GetCount => Events.Count;

    internal void Initialize()
    {
        Events = new List<CustomEvent>();
        try
        {

            if (File.Exists($"{State.StorageDirectory}events.txt"))
            {
                Encoding encoding = Encoding.GetEncoding("iso-8859-1");
                var logFile = File.ReadAllLines($"{State.StorageDirectory}events.txt", encoding);
                CustomEvent current = new CustomEvent();
                int line = 10;
                foreach (string entry in logFile)
                {
                    if (entry == "Event")
                    {
                        current = new CustomEvent();
                        line = 0;
                    }
                    switch (line)
                    {
                        case 1:
                            current.MainText = entry;
                            break;
                        case 2:
                            current.Option1Choice = entry;
                            break;
                        case 3:
                            current.Option1Result = entry;
                            break;
                        case 4:
                            {
                                var sections = entry.Split(',');
                                if (sections.Length > 0) current.Option1Gold = int.Parse(sections[0]);
                                if (sections.Length > 1) current.Option1Happiness = int.Parse(sections[1]);
                                if (sections.Length > 2) current.Option1HappinessAll = int.Parse(sections[2]);
                                if (sections.Length > 3) current.Option1Population = int.Parse(sections[3]);
                                if (sections.Length > 4) current.Option1Relations = float.Parse(sections[4]);
                            }
                            break;
                        case 5:
                            current.Option2Choice = entry;
                            break;
                        case 6:
                            current.Option2Result = entry;
                            break;
                        case 7:
                            {
                                var sections = entry.Split(',');
                                if (sections.Length > 0) current.Option2Gold = int.Parse(sections[0]);
                                if (sections.Length > 1) current.Option2Happiness = int.Parse(sections[1]);
                                if (sections.Length > 2) current.Option2HappinessAll = int.Parse(sections[2]);
                                if (sections.Length > 3) current.Option2Population = int.Parse(sections[3]);
                                if (sections.Length > 4) current.Option2Relations = int.Parse(sections[4]);
                            }
                            Events.Add(current);
                            break;

                    }
                    line += 1;

                }
            }
        }
        catch
        {
            var manager = UnityEngine.Object.FindObjectOfType<GameManager>(); //Done this way because State.Gamemanager doesn't exist when this is called
            manager.CreateMessageBox("Exception reading in the events.txt file, make sure that your events are following the guidelines, or you can delete it and the game will copy a fresh copy from the StreamingAssets folder");
        }
    }


}
