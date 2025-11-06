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
    public InputField Confidence;
    public InputField MinArmySize;
    public InputField MaxArmySize;
    public InputField TurnOrder;
    public Toggle AddonRace;
    public Toggle MonsterScoutMP;
    public Dropdown ConquestType;
    public Dropdown DayNightMonsterMovemnt;
}

