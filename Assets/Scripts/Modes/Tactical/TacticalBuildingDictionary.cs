using System.Linq;
using UnityEngine;

public class TacticalBuildingDictionary : MonoBehaviour
{
    public Sprite[] LogCabins;
    public Sprite[] WoodenBuildings;
    public Sprite[] HarpyNests;
    public Sprite[] LamiaTemple;
    public Sprite[] Buildings;
    internal Sprite[] AllSprites;

    private void Awake()
    {
        AllSprites = LogCabins.Concat(WoodenBuildings).Concat(HarpyNests).Concat(Buildings).Concat(LamiaTemple).ToArray();
    }

}
