using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterSpawnerPanel : MonoBehaviour
{
    internal Race race;
    public Toggle SpawnEnabled;
    public Slider SpawnRate;
    public InputField MaxArmies;
    public InputField ScalingRate;
    public InputField Team;
    public InputField SpawnAttempts;
    public InputField MinArmySize;
    public InputField MaxArmySize;
    public InputField TurnOrder;
    public Toggle AddonRace;
    public Dropdown ConquestType;
}

