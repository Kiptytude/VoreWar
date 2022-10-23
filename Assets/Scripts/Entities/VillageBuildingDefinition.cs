using System.Collections.Generic;
using System.Linq;

public class VillageBuildingDefinition
{
    internal VillageBuilding Id;
    internal string Name;
    internal string Description;
	
    internal BuildingCost Cost = new BuildingCost();
    internal bool CanBeBuilt = true;
	
    internal VillageBoosts Boosts = new VillageBoosts();
	
    internal List<VillageBuilding> RequiredBuildings = new List<VillageBuilding>();
    internal bool RequiresSubjugatedRace = false;
    internal bool RequiresRaceCapitol = false;
	
	internal bool RemovedOnOwnerChange = false;
	internal bool AddedOnOriginalOwner = false;
	
    public VillageBuildingDefinition(VillageBuilding id, string name, string desc)
    {
        Id = id;
        Name = name;
        Description = desc;
    }
	
    static public BuildingCost GetCost(VillageBuilding building, int amount = 1)
    {
        var def = VillageBuildingList.GetBuildingDefinition(building);
        return GetCost(def, amount);
    }

    static public BuildingCost GetCost(VillageBuildingDefinition buildingDef, int amount = 1)
    {
        var cost = new BuildingCost() {
            Wealth = buildingDef.Cost.Wealth,
            LeaderExperience = buildingDef.Cost.LeaderExperience
        };
        cost.Wealth *= amount;
        cost.LeaderExperience *= (float)amount;
        return cost;
    }

	public bool CanAfford(Empire empire)
	{
        var hasEnoughWealth = (empire.Gold >= Cost.Wealth || Cost.Wealth <= 0);
        var hasEnoughLeaderXp = (Cost.LeaderExperience <= 0.0f || (empire.Leader?.IsDead == false && empire.Leader?.Experience > Cost.LeaderExperience));
        return hasEnoughWealth && hasEnoughLeaderXp;
	}
	
	public bool CanBuild(Village village)
	{
		if (CanBeBuilt == false) return false;
		if (village.buildings.Contains(Id) == true) return false;
		

		var subjugationRequirementMet = (RequiresSubjugatedRace == false) || (village.Side != (int)village.Race);
		if (subjugationRequirementMet == false) return false;
		
		if (RequiresRaceCapitol && (village.Capital == false || village.OriginalRace != village.Race)) return false;
		
		return HasAllPrerequisites(village);
    }

    public bool CanEverBuild(Village village)
    {
        if (CanBeBuilt == false)
        {
            return false;
        }
        if (village.buildings.Contains(Id))
        {
            return false;
        }
        if (RequiresSubjugatedRace && village.IsSubjugated() == false)
        {
            return false;
        }
        if (RequiresRaceCapitol && (
            village.IsOriginalOwner() == false
            || village.Capital == false)
            )
        {
            return false;
        }
        return true;
    }

    public bool HasAllPrerequisites(Village village)
	{
		foreach (var requirement in RequiredBuildings)
		{
			if (village.buildings.Contains(requirement) == false)
			{
				return false;
			}
		}
		
		return true;
	}
	
	public string GetFirstUnmetPrerequisiteName(List<VillageBuilding> builtBuildings)
	{
		foreach (var requirement in RequiredBuildings)
		{
			if (builtBuildings.Contains(requirement) == false)
			{
				var buildingDef = VillageBuildingList.GetBuildingDefinition(requirement);
				return buildingDef.Name;
			}
		}
		
		return "";
	}
}
