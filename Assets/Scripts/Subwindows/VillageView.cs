using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class VillageView
{
    Village village;
    VillageViewPanel UI;

    Empire BuyingEmpire;

    Button[] buttons;

    public VillageView(Village village, VillageViewPanel villageUI)
    {
        buttons = new Button[(int)VillageBuilding.LastIndex + 1];
        UI = villageUI;
    }

    internal void Open(Village village, Empire activatingEmpire)
    {
        BuyingEmpire = activatingEmpire;
        UI.BuyForAllToggle.isOn = false;
        this.village = village;
        Refresh();
        UI.gameObject.SetActive(true);

    }

    internal void Refresh()
    {
        if (UI.BuyForAllToggle.isOn)
            CreateOrUpdateBuyEachButtons();
        else
            CreateOrUpdateButtons();
        GenerateText();
    }

    void GenerateText()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Village: {village.Name}");
        sb.AppendLine($"Population: {village.GetTotalPop()}");
        sb.AppendLine($"Maximum Population: {village.Maxpop}");
        sb.AppendLine($"Garrison: {village.Garrison}");
        sb.AppendLine($"Income: {village.GetIncome()}");
        sb.AppendLine($"Current Money: {BuyingEmpire.Gold}");
        sb.AppendLine($"Starting Experience: {village.GetStartingXp()}");
        if (village.NetBoosts.FarmsEquivalent > 0) sb.AppendLine($"Extra Farms: {village.NetBoosts.FarmsEquivalent}");
        if (village.NetBoosts.WealthMult > 1) sb.AppendLine($"Gold Income: {100 * village.NetBoosts.WealthMult}%");
        if (village.NetBoosts.WealthAdd > 0) sb.AppendLine($"Gold Income: +{village.NetBoosts.WealthAdd}");
        if (village.NetBoosts.PopulationMaxMult > 1) sb.AppendLine($"Population Max: {100 * village.NetBoosts.PopulationMaxMult}%");
        if (village.NetBoosts.PopulationMaxAdd > 0) sb.AppendLine($"Population Max: +{village.NetBoosts.PopulationMaxAdd}");
        if (village.NetBoosts.GarrisonMaxMult > 1) sb.AppendLine($"Max Garrison: {100 * village.NetBoosts.GarrisonMaxMult}%");
        if (village.NetBoosts.GarrisonMaxAdd > 0) sb.AppendLine($"Max Garrison: +{village.NetBoosts.GarrisonMaxAdd}");

        if (village.buildings.Any())
        {
            sb.AppendLine($"Buildings already built:");
            foreach (var buildingId in village.buildings)
            {
                var buildingDef = VillageBuildingList.GetBuildingDefinition(buildingId);
                if (buildingDef != null)
                    sb.AppendLine($"{buildingDef.Name} : {buildingDef.Description}");
            }
        }
        UI.VillageInfo.text = sb.ToString();
    }

    void CreateOrUpdateButtons()
    {
        ClearAllButtons();
        int nextOpenPos = 0;

        foreach (var building in VillageBuildingList.GetListOfBuildingEnum())
        {
            var buildingDef = VillageBuildingList.GetBuildingDefinition(building);
            var added = SetButton(buildingDef, nextOpenPos);
            if (added)
                nextOpenPos++;
        }
        SetBuyAllButton(nextOpenPos);
    }

    bool SetButton(VillageBuildingDefinition buildingDef, int buttonPosition)
    {
        if (buildingDef.CanBeBuilt == false)
        {
            return false;
        }
        if (buildingDef.CanEverBuild(village) == false)
        {
            return false;
        }

        string text = buildingDef.Name + " - " + buildingDef.Description + " - Cost: " + GetCostText(buildingDef.Cost);
        Button button = ResetButton(buttonPosition);

        if (buildingDef.HasAllPrerequisites(village) == false)
        {
            button.interactable = false;
            button.GetComponentInChildren<Text>().text = "Requires " + buildingDef.GetFirstUnmetPrerequisiteName(village.buildings) + " to be built first. " + text;
        }
        else if (buildingDef.CanAfford(BuyingEmpire) == false)
        {
            button.interactable = false;
            button.GetComponentInChildren<Text>().text = "Cannot afford " + text;
        }
        else if (buildingDef.CanBuild(village) == false)
        {
            button.interactable = false;
            button.GetComponentInChildren<Text>().text = "ERROR: Cannot build " + text;
        }
        else
        {
            button.interactable = true;
            button.onClick.AddListener(() => Build(buildingDef.Id));
            button.GetComponentInChildren<Text>().text = "Build " + text;
        }
        return true;
    }

    void CreateOrUpdateBuyEachButtons()
    {
        ClearAllButtons();
        int nextOpenPos = 0;

        Village[] villages = State.World.Villages.Where(s => s.Side == village.Side).ToArray();

        foreach (var building in VillageBuildingList.GetListOfBuildingEnum())
        {
            var buildingDef = VillageBuildingList.GetBuildingDefinition(building);
            var anyVillageStillNeeds = false;
            var anyVillageCanBuild = false;
            foreach (var villageToCheck in villages)
            {
                if (villageToCheck.buildings.Contains(building) == false)
                {
                    anyVillageStillNeeds = true;
                }
                if (buildingDef.CanBuild(villageToCheck))
                {
                    anyVillageCanBuild = true;
                }
                if (anyVillageStillNeeds && anyVillageCanBuild)
                {
                    break;
                }
            }

            if (anyVillageStillNeeds && anyVillageCanBuild)
            {
                var added = SetButtonForEachVillage(buildingDef.Name + " - " + buildingDef.Description + " - Cost: " + GetCostText(VillageBuildingDefinition.GetCost(building)), building, nextOpenPos, villages);
                if (added)
                    nextOpenPos++;
            }
        }
        SetAllBuyAllButton(nextOpenPos, villages);
    }

    bool SetButtonForEachVillage(string text, VillageBuilding building, int buttonPosition, Village[] villages)
    {
        if (village.Empire != BuyingEmpire)
        {
            State.GameManager.CreateMessageBox("Can't use buy for each village for allied empires, to avoid confusion about what the correct behavior should be");
            return false;
        }
        var buildingDef = VillageBuildingList.GetBuildingDefinition(building);
        Button button = ResetButton(buttonPosition);
        int needed = 0;
        foreach (Village villageToCheck in villages)
        {
            if (villageToCheck.buildings.Contains(building) == false)
            {
                if (buildingDef.CanBuild(villageToCheck))
                {
                    needed++;
                }
            }
        }
        if (needed > 0)
        {
            var cost = VillageBuildingDefinition.GetCost(building, needed);
            if (buildingDef.CanAfford(BuyingEmpire) == false)
            {
                button.interactable = false;
                button.GetComponentInChildren<Text>().text = $"{text} * {needed} = {GetCostText(cost)} -- Cannot afford ";
            }
            else
            {
                button.interactable = true;
                button.onClick.AddListener(() => BuildForEachVillage(building));
                button.GetComponentInChildren<Text>().text = $"Build {text} * {needed} = {GetCostText(cost)}";
            }
        }
        else
        {
            return false;
        }
        return true;
    }

    private void ClearAllButtons()
    {
        for (var i = 0; i < buttons.Length; i++)
        {
            var button = ResetButton(i);
            button.GetComponent<Button>().interactable = false;
            button.GetComponentInChildren<Text>().text = "...";
            button.gameObject.SetActive(false);
        }
    }

    private Button ResetButton(int position)
    {
        int id = position;
        Button button;
        if (buttons[id] == null)
        {
            button = Object.Instantiate(UI.ButtonPrefab, UI.ButtonPanel.transform).GetComponent<Button>();
            buttons[id] = button;
        }
        else
        {
            button = buttons[id];
            button.onClick.RemoveAllListeners();
        }
        button.gameObject.SetActive(true);

        return button;
    }

    void SetBuyAllButton(int buttonPosition)
    {
        Button button = ResetButton(buttonPosition);
        button.gameObject.SetActive(true);

        button.onClick.AddListener(() => BuildAll());

        var cost = village.GetCostAllBuildings();
        if (BuyingEmpire == null || cost == null)
        {
            Debug.Log("This shouldn't have happened");
            button.interactable = false;
            return;
        }
        if (cost.Wealth == 0 && cost.LeaderExperience == 0)
        {
            button.interactable = false;
            button.GetComponentInChildren<Text>().text = "This village already has all of the buildings";
        }
        else if (BuyingEmpire.Gold < cost.Wealth || BuyingEmpire.Leader?.Experience < cost.LeaderExperience)
        {
            button.interactable = false;
            button.GetComponentInChildren<Text>().text = $"Cannot afford to buy all buildings - {GetCostText(cost)}";
        }
        else
        {
            button.interactable = true;
            button.GetComponentInChildren<Text>().text = $"Build All buildings - {GetCostText(cost)}";
        }
    }

    void SetAllBuyAllButton(int buttonPosition, Village[] villages)
    {
        Button button = ResetButton(buttonPosition);
        button.gameObject.SetActive(true);

        button.onClick.AddListener(() => BuildAllForAll());


        BuildingCost cost = village.GetCostAllBuildings();
        foreach (Village village in villages)
        {
            var localcost = village.GetCostAllBuildings();
            cost.LeaderExperience += localcost.LeaderExperience;
            cost.Wealth += localcost.Wealth;
        }
        if (BuyingEmpire == null || cost == null)
        {
            Debug.Log("This shouldn't have happened");
            button.interactable = false;
            return;
        }
        if (cost.Wealth == 0 && cost.LeaderExperience == 0)
        {
            button.interactable = false;
            button.GetComponentInChildren<Text>().text = "No village needs a building";
        }
        else if (BuyingEmpire.Gold < cost.Wealth || BuyingEmpire.Leader?.Experience < cost.LeaderExperience)
        {
            button.interactable = false;
            button.GetComponentInChildren<Text>().text = $"Cannot afford to buy all buildings for all villages {GetCostText(cost)}";
        }
        else
        {
            button.interactable = true;
            button.GetComponentInChildren<Text>().text = $"Build all buildings for all villages - {GetCostText(cost)}";
        }
    }

    private string GetCostText(BuildingCost cost)
    {
        var textResult = "";
        if (cost.Wealth > 0) textResult += "" + cost.Wealth + " Gold";
        if (cost.LeaderExperience > 0.0f)
        {
            if (textResult != "") textResult += " and ";
            textResult += "" + (int)cost.LeaderExperience + " Leader XP";
        }
        return textResult;
    }

    void BuildAll()
    {
        village.BuyAllBuildings(BuyingEmpire);
        Refresh();
    }

    void BuildAllForAll()
    {
        foreach (Village vill in State.World.Villages.Where(s => s.Side == village.Side))
        {
            vill.BuyAllBuildings(BuyingEmpire);
        }
        Refresh();
    }

    void BuildForEachVillage(VillageBuilding building)
    {
        foreach (Village vill in State.World.Villages.Where(s => s.Side == village.Side && s.buildings.Contains(building) == false))
        {
            vill.Build(building, BuyingEmpire);
        }
        Refresh();
    }

    void Build(VillageBuilding building)
    {
        village.Build(building, BuyingEmpire);
        Refresh();
    }

}
