using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class StartTacticalUI : MonoBehaviour
{

    public Toggle AIPlayer;
    public Dropdown TacticalAI;
    public Dropdown Race;
    public Toggle CanVore;

    public Toggle HasLeader;

    public Text RangedText;
    public Text WeaponsText;
    public Text MagicText;

    public InputField UnitCount;
    public InputField Level;
    public Slider RangedPercentage;
    public Slider HeavyWeaponsPercentage;
    public Slider MagicPercentage;
    public InputField MaxSpellLevel;


}