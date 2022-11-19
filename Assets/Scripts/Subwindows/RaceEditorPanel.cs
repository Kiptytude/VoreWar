using UnityEngine;
using System.Collections;
using TMPro;
using System;
using System.Linq;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text;

public class RaceEditorPanel : MonoBehaviour
{

    public TMP_Dropdown RaceDropdown;
    public TMP_Dropdown DisplayRaceDropdown;

    public Toggle OverrideGender;

    public Slider MaleFraction;
    public Slider HermFraction;

    public Toggle OverrideFurry;
    public Slider FurryFraction;

    public InputField BodySize;
    public InputField StomachSize;

    public TMP_Dropdown InnateSpellDropdown;

    public TMP_Dropdown TraitDropdown;
    public TextMeshProUGUI TraitList;

    public Toggle UnbirthDisabled;
    public Toggle CockVoreDisabled;
    public Toggle BreastVoreDisabled;
    public Toggle TailVoreDisabled;
    public Toggle AnalVoreDisabled;

    public InputField MinStrength;
    public InputField MaxStrength;
    public InputField MinDexterity;
    public InputField MaxDexterity;
    public InputField MinEndurance;
    public InputField MaxEndurance;
    public InputField MinMind;
    public InputField MaxMind;
    public InputField MinWill;
    public InputField MaxWill;
    public InputField MinAgility;
    public InputField MaxAgility;
    public InputField MinVoracity;
    public InputField MaxVoracity;
    public InputField MinStomach;
    public InputField MaxStomach;

    public Toggle OverrideClothed;
    public Slider ClothedFraction;
    public Toggle OverrideWeight;
    public InputField MinWeight;
    public InputField MaxWeight;
    public Toggle OverrideBoob;
    public InputField MinBoob;
    public InputField MaxBoob;
    public Toggle OverrideDick;
    public InputField MinDick;
    public InputField MaxDick;

    public Text WeightText;
    public Text BoobText;
    public Text DickText;

    public InputField MaleTraits;
    public InputField FemaleTraits;
    public InputField HermTraits;

    public TMP_Dropdown BannerType;

    public TMP_Dropdown FavoredStat;

    public TMP_Dropdown RaceAIDropdown;

    List<Traits> CurrentTraits;


    Race PreviousRace = (Race)(-1);


    internal void ShowPanel()
    {
        if (RaceDropdown.options?.Any() == false)
        {
            foreach (Race race in ((Race[])Enum.GetValues(typeof(Race))).OrderBy((s) => s.ToString()))
            {
                RaceDropdown.options.Add(new TMP_Dropdown.OptionData(race.ToString()));
            }
            RaceDropdown.RefreshShownValue();
        }

        if (TraitDropdown.options?.Any() == false)
        {
            foreach (Traits traitId in ((Traits[])Enum.GetValues(typeof(Traits))).OrderBy(s =>
             {
                 return s >= Traits.LightningSpeed ? "ZZZ" + s.ToString() : s.ToString();
             }))
            {
                TraitDropdown.options.Add(new TMP_Dropdown.OptionData(traitId.ToString()));
            }
            TraitDropdown.RefreshShownValue();
        }

        if (FavoredStat.options?.Any() == false)
        {
            foreach (Stat stat in ((Stat[])Enum.GetValues(typeof(Traits))).Where((s) => s < Stat.Leadership))
            {
                FavoredStat.options.Add(new TMP_Dropdown.OptionData(stat.ToString()));
            }
            FavoredStat.RefreshShownValue();
        }

        if (InnateSpellDropdown.options?.Any() == false)
        {
            foreach (SpellTypes type in ((SpellTypes[])Enum.GetValues(typeof(SpellTypes))).Where(s => (int)s < 100))
            {
                InnateSpellDropdown.options.Add(new TMP_Dropdown.OptionData(type.ToString()));
            }
            InnateSpellDropdown.RefreshShownValue();
        }

        if (BannerType.options.Count < 4)
        {
            foreach (BannerTypes type in (BannerTypes[])Enum.GetValues(typeof(BannerTypes)))
            {
                BannerType.options.Add(new TMP_Dropdown.OptionData(type.ToString()));
            }
            for (int i = 0; i < CustomBannerTest.Sprites.Length; i++)
            {
                if (CustomBannerTest.Sprites[i] == null)
                    break;
                BannerType.options.Add(new TMP_Dropdown.OptionData($"Custom {i + 1}"));

            }
        }

        if (RaceAIDropdown.options?.Any() == false)
        {
            foreach (RaceAI raceAI in ((RaceAI[])Enum.GetValues(typeof(RaceAI))))
            {
                RaceAIDropdown.options.Add(new TMP_Dropdown.OptionData(raceAI.ToString()));
            }
            RaceAIDropdown.RefreshShownValue();
        }

        LoadRace();
        UpdateInteractable();

    }

    public void RaceChanged()
    {
        SaveRace();
        LoadRace();
        UpdateInteractable();
    }


    public void AddTrait()
    {

        if (Enum.TryParse(TraitDropdown.options[TraitDropdown.value].text, out Traits trait))
        {
            if (CurrentTraits.Contains(trait) == false)
                CurrentTraits.Add(trait);
        }
        UpdateInteractable();
    }

    public void AddTraitALL()
    {
        if (Enum.TryParse(TraitDropdown.options[TraitDropdown.value].text, out Traits trait))
        {
            foreach (Race race in (Race[])Enum.GetValues(typeof(Race)))
            {
                RaceSettingsItem item = State.RaceSettings.Get(race);
                if (item.RaceTraits.Contains(trait) == false)
                    item.RaceTraits.Add(trait);
            }
            AddTrait();
        }

    }

    public void RemoveTrait()
    {

        if (Enum.TryParse(TraitDropdown.options[TraitDropdown.value].text, out Traits trait))
        {
            CurrentTraits.Remove(trait);
        }
        UpdateInteractable();
    }

    public void RemoveTraitALL()
    {
        if (Enum.TryParse(TraitDropdown.options[TraitDropdown.value].text, out Traits trait))
        {
            foreach (Race race in (Race[])Enum.GetValues(typeof(Race)))
            {
                RaceSettingsItem item = State.RaceSettings.Get(race);
                item.RaceTraits.Remove(trait);
            }
            RemoveTrait();
        }

    }

    internal void SaveRace()
    {
        try
        {
            if (PreviousRace != (Race)(-1))
            {
                RaceSettingsItem item = State.RaceSettings.Get(PreviousRace);
                var racePar = RaceParameters.GetRaceTraits(PreviousRace);
                var raceData = Races.GetRace(PreviousRace);
                item.OverrideGender = OverrideGender.isOn;
                item.OverrideFurry = OverrideFurry.isOn;
                item.maleFraction = 1 - MaleFraction.value;
                item.hermFraction = HermFraction.value;
                item.furryFraction = FurryFraction.value;

                item.BannerType = BannerType.value;
                item.RaceAI = (RaceAI)RaceAIDropdown.value;

                item.overrideClothes = OverrideClothed.isOn;
                item.clothedFraction = ClothedFraction.value;

                item.overrideWeight = OverrideWeight.isOn;
                item.MinWeight = Convert.ToInt32(MinWeight.text) - 1;
                item.MaxWeight = Convert.ToInt32(MaxWeight.text) - 1;

                item.FavoredStat = (Stat)FavoredStat.value;
                item.FavoredStatSet = true;

                var value = (SpellTypes)InnateSpellDropdown.value;

                if (value > SpellTypes.Resurrection)
                    value = value - SpellTypes.Resurrection + SpellTypes.AlraunePuff - 1;

                item.InnateSpell = value;

                item.overrideBoob = OverrideBoob.isOn;
                item.MinBoob = Convert.ToInt32(MinBoob.text) - 1;
                item.MaxBoob = Convert.ToInt32(MaxBoob.text) - 1;

                item.overrideDick = OverrideDick.isOn;
                item.MinDick = Convert.ToInt32(MinDick.text) - 1;
                item.MaxDick = Convert.ToInt32(MaxDick.text) - 1;

                if (item.MinWeight < 0)
                    item.MinWeight = 0;
                if (item.MaxWeight >= raceData.BodySizes)
                    item.MaxWeight = Math.Max(raceData.BodySizes - 1, 0);
                if (item.MinWeight > item.MaxWeight)
                    item.MinWeight = item.MaxWeight;

                if (item.MinBoob < 0)
                    item.MinBoob = 0;
                if (item.MaxBoob >= raceData.BreastSizes)
                    item.MaxBoob = Math.Max(raceData.BreastSizes - 1, 0);
                if (item.MinBoob > item.MaxBoob)
                    item.MinBoob = item.MaxBoob;

                if (item.MinDick < 0)
                    item.MinDick = 0;
                if (item.MaxDick >= raceData.DickSizes)
                    item.MaxDick = Math.Max(raceData.DickSizes - 1, 0);
                if (item.MinDick > item.MaxDick)
                    item.MinDick = item.MaxDick;



                item.BodySize = Convert.ToInt32(BodySize.text);
                item.StomachSize = Convert.ToInt32(StomachSize.text);

                if (item.BodySize < 1)
                    item.BodySize = 1;
                if (item.StomachSize < 1)
                    item.StomachSize = 1;



                item.RaceTraits = CurrentTraits.ToList();
                List<VoreType> newtypes = racePar.AllowedVoreTypes.ToList();
                if (UnbirthDisabled.isOn) newtypes.Remove(VoreType.Unbirth);
                if (CockVoreDisabled.isOn) newtypes.Remove(VoreType.CockVore);
                if (AnalVoreDisabled.isOn) newtypes.Remove(VoreType.Anal);
                if (BreastVoreDisabled.isOn) newtypes.Remove(VoreType.BreastVore);
                if (TailVoreDisabled.isOn) newtypes.Remove(VoreType.TailVore);
                item.AllowedVoreTypes = newtypes;

                item.Stats.Strength.Minimum = Convert.ToInt32(MinStrength.text);
                item.Stats.Strength.Roll = 1 + Convert.ToInt32(MaxStrength.text) - item.Stats.Strength.Minimum;
                if (item.Stats.Strength.Roll < 1) item.Stats.Strength.Roll = 1;
                item.Stats.Dexterity.Minimum = Convert.ToInt32(MinDexterity.text);
                item.Stats.Dexterity.Roll = 1 + Convert.ToInt32(MaxDexterity.text) - item.Stats.Dexterity.Minimum;
                if (item.Stats.Dexterity.Roll < 1) item.Stats.Strength.Roll = 1;
                item.Stats.Endurance.Minimum = Convert.ToInt32(MinEndurance.text);
                item.Stats.Endurance.Roll = 1 + Convert.ToInt32(MaxEndurance.text) - item.Stats.Endurance.Minimum;
                if (item.Stats.Endurance.Roll < 1) item.Stats.Strength.Roll = 1;
                item.Stats.Mind.Minimum = Convert.ToInt32(MinMind.text);
                item.Stats.Mind.Roll = 1 + Convert.ToInt32(MaxMind.text) - item.Stats.Mind.Minimum;
                if (item.Stats.Mind.Roll < 1) item.Stats.Strength.Roll = 1;
                item.Stats.Will.Minimum = Convert.ToInt32(MinWill.text);
                item.Stats.Will.Roll = 1 + Convert.ToInt32(MaxWill.text) - item.Stats.Will.Minimum;
                if (item.Stats.Will.Roll < 1) item.Stats.Strength.Roll = 1;
                item.Stats.Agility.Minimum = Convert.ToInt32(MinAgility.text);
                item.Stats.Agility.Roll = 1 + Convert.ToInt32(MaxAgility.text) - item.Stats.Agility.Minimum;
                if (item.Stats.Agility.Roll < 1) item.Stats.Strength.Roll = 1;
                item.Stats.Voracity.Minimum = Convert.ToInt32(MinVoracity.text);
                item.Stats.Voracity.Roll = 1 + Convert.ToInt32(MaxVoracity.text) - item.Stats.Voracity.Minimum;
                if (item.Stats.Voracity.Roll < 1) item.Stats.Strength.Roll = 1;
                item.Stats.Stomach.Minimum = Convert.ToInt32(MinStomach.text);
                item.Stats.Stomach.Roll = 1 + Convert.ToInt32(MaxStomach.text) - item.Stats.Stomach.Minimum;
                if (item.Stats.Stomach.Roll < 1) item.Stats.Strength.Roll = 1;

                item.FemaleTraits = TextToTraitList(FemaleTraits.text);
                item.MaleTraits = TextToTraitList(MaleTraits.text);
                item.HermTraits = TextToTraitList(HermTraits.text);

            }
        }
        catch
        {
            State.GameManager.CreateMessageBox("There's a input box that's not filled in");
        }

    }

    public void OpenImportTraitList()
    {
        var box = State.GameManager.CreateInputBox();
        box.SetData(SetBaseTraitsToImportedList, "Import", "Cancel", "Insert a list of space separated traits here to replace the current list of traits for this race with that list", 120);
    }

    void SetBaseTraitsToImportedList(string traits)
    {
        CurrentTraits = TextToTraitList(traits);
        UpdateInteractable();
    }

    public static List<Traits> TextToTraitList(string text)
    {
        List<Traits> traits = new List<Traits>();
        foreach (Traits trait in (Stat[])Enum.GetValues(typeof(Traits)))
        {
            if (text.ToLower().Contains(trait.ToString().ToLower()))
            {
                traits.Add(trait);
            }
        }
        traits = traits.Distinct().ToList();
        return traits;
    }

  

    public static string TraitListToText(List<Traits> traits)
    {
        if (traits == null)
            return "";
        string ret = "";
        bool first = true;
        foreach (var trait in traits)
        {
            if (first)
                first = false;
            else
                ret += ", ";
            ret += trait.ToString();
        }
        return ret;
    }

    internal void LoadRace()
    {
        if (Enum.TryParse(RaceDropdown.options[RaceDropdown.value].text, out Race race))
        {
            PreviousRace = race;
            RaceSettingsItem item = State.RaceSettings.Get(race);
            var racePar = RaceParameters.GetRaceTraits(race);
            var raceData = Races.GetRace(race);
            OverrideGender.isOn = item.OverrideGender;
            OverrideFurry.isOn = item.OverrideFurry;
            MaleFraction.value = 1 - item.maleFraction;
            HermFraction.value = item.hermFraction;
            FurryFraction.value = item.furryFraction;

            BannerType.value = item.BannerType;

            RaceAIDropdown.value = (int)item.RaceAI;
            RaceAIDropdown.RefreshShownValue();

            OverrideClothed.isOn = item.overrideClothes;
            ClothedFraction.value = item.clothedFraction;

            OverrideWeight.isOn = item.overrideWeight;
            MinWeight.text = (1 + item.MinWeight).ToString();
            MaxWeight.text = (1 + item.MaxWeight).ToString();

            OverrideBoob.isOn = item.overrideBoob;
            MinBoob.text = (1 + item.MinBoob).ToString();
            MaxBoob.text = (1 + item.MaxBoob).ToString();

            OverrideDick.isOn = item.overrideDick;
            MinDick.text = (1 + item.MinDick).ToString();
            MaxDick.text = (1 + item.MaxDick).ToString();

            WeightText.text = $"Weight 1 - {Math.Max(raceData.BodySizes, 1)}";
            BoobText.text = $"Breasts 1 - {Math.Max(raceData.BreastSizes, 1)}";
            DickText.text = $"Dick 1 - {Math.Max(raceData.DickSizes, 1)}";

            FavoredStat.value = (int)State.RaceSettings.GetFavoredStat(race);
            FavoredStat.RefreshShownValue();

            var spell = State.RaceSettings.GetInnateSpell(race);
            if (spell > SpellTypes.Resurrection)
                spell = spell - SpellTypes.AlraunePuff + SpellTypes.Resurrection + 1;


            InnateSpellDropdown.value = (int)spell;

            InnateSpellDropdown.RefreshShownValue();


            BodySize.text = item.BodySize.ToString();
            StomachSize.text = item.StomachSize.ToString();

            CurrentTraits = item.RaceTraits.ToList();
            if (CurrentTraits == null)
                CurrentTraits = new List<Traits>();
            UnbirthDisabled.isOn = !item.AllowedVoreTypes.Contains(VoreType.Unbirth);
            UnbirthDisabled.interactable = racePar.AllowedVoreTypes.Contains(VoreType.Unbirth);
            CockVoreDisabled.isOn = !item.AllowedVoreTypes.Contains(VoreType.CockVore);
            CockVoreDisabled.interactable = racePar.AllowedVoreTypes.Contains(VoreType.CockVore);
            AnalVoreDisabled.isOn = !item.AllowedVoreTypes.Contains(VoreType.Anal);
            AnalVoreDisabled.interactable = racePar.AllowedVoreTypes.Contains(VoreType.Anal);
            BreastVoreDisabled.isOn = !item.AllowedVoreTypes.Contains(VoreType.BreastVore);
            BreastVoreDisabled.interactable = racePar.AllowedVoreTypes.Contains(VoreType.BreastVore);
            TailVoreDisabled.isOn = !item.AllowedVoreTypes.Contains(VoreType.TailVore);
            TailVoreDisabled.interactable = racePar.AllowedVoreTypes.Contains(VoreType.TailVore);

            MinStrength.text = item.Stats.Strength.Minimum.ToString();
            MaxStrength.text = (item.Stats.Strength.Minimum + item.Stats.Strength.Roll - 1).ToString();
            MinDexterity.text = item.Stats.Dexterity.Minimum.ToString();
            MaxDexterity.text = (item.Stats.Dexterity.Minimum + item.Stats.Dexterity.Roll - 1).ToString();
            MinEndurance.text = item.Stats.Endurance.Minimum.ToString();
            MaxEndurance.text = (item.Stats.Endurance.Minimum + item.Stats.Endurance.Roll - 1).ToString();
            MinMind.text = item.Stats.Mind.Minimum.ToString();
            MaxMind.text = (item.Stats.Mind.Minimum + item.Stats.Mind.Roll - 1).ToString();
            MinWill.text = item.Stats.Will.Minimum.ToString();
            MaxWill.text = (item.Stats.Will.Minimum + item.Stats.Will.Roll - 1).ToString();
            MinAgility.text = item.Stats.Agility.Minimum.ToString();
            MaxAgility.text = (item.Stats.Agility.Minimum + item.Stats.Agility.Roll - 1).ToString();
            MinVoracity.text = item.Stats.Voracity.Minimum.ToString();
            MaxVoracity.text = (item.Stats.Voracity.Minimum + item.Stats.Voracity.Roll - 1).ToString();
            MinStomach.text = item.Stats.Stomach.Minimum.ToString();
            MaxStomach.text = (item.Stats.Stomach.Minimum + item.Stats.Stomach.Roll - 1).ToString();

            FemaleTraits.text = TraitListToText(item.FemaleTraits);
            MaleTraits.text = TraitListToText(item.MaleTraits);
            HermTraits.text = TraitListToText(item.HermTraits);
        }
    }

    public void UpdateInteractable()
    {
        var raceData = Races.GetRace(PreviousRace);

        if (raceData == null)
        {
            OverrideGender.isOn = false;
            OverrideFurry.isOn = false;
            return;
        }

        if (raceData.CanBeGender.Count() > 1)
        {
            OverrideGender.interactable = true;
        }
        else
        {
            OverrideGender.interactable = false;
            OverrideGender.isOn = false;
        }

        if (raceData.FurCapable)
        {
            OverrideFurry.interactable = true;
        }
        else
        {
            OverrideFurry.interactable = false;
            OverrideFurry.isOn = false;
        }

        if (raceData.BreastSizes > 0)
        {
            OverrideBoob.interactable = true;
        }
        else
        {
            OverrideBoob.interactable = false;
            OverrideBoob.isOn = false;
        }

        if (raceData.DickSizes > 0)
        {
            OverrideDick.interactable = true;
        }
        else
        {
            OverrideDick.interactable = false;
            OverrideDick.isOn = false;
        }

        if (raceData.BreastSizes > 0)
        {
            OverrideDick.interactable = true;
        }
        else
        {
            OverrideDick.interactable = false;
            OverrideDick.isOn = false;
        }

        if (raceData.AllowedMainClothingTypes?.Count > 0)
        {
            OverrideClothed.interactable = true;
        }
        else
        {
            OverrideClothed.interactable = false;
            OverrideClothed.isOn = false;
        }

        BannerType.interactable = PreviousRace < Race.Succubi;



        MaleFraction.interactable = OverrideGender.isOn;
        HermFraction.interactable = OverrideGender.isOn;
        FurryFraction.interactable = OverrideFurry.isOn;

        ClothedFraction.interactable = OverrideClothed.isOn;
        MinWeight.interactable = OverrideWeight.isOn;
        MaxWeight.interactable = OverrideWeight.isOn;
        MinBoob.interactable = OverrideBoob.isOn;
        MaxBoob.interactable = OverrideBoob.isOn;
        MinDick.interactable = OverrideDick.isOn;
        MaxDick.interactable = OverrideDick.isOn;


        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Traits:");
        if (CurrentTraits == null)
            CurrentTraits = new List<Traits>();
        foreach (Traits trait in CurrentTraits)
        {
            sb.AppendLine(trait.ToString());
        }
        TraitList.text = sb.ToString();
    }

    public void CloseAndSave()
    {
        SaveRace();
        State.SaveEditedRaces();
        gameObject.SetActive(false);
        if (State.GameManager.CurrentScene == State.GameManager.Start_Mode)
            return;
        if (State.World.Villages != null)
        {
            var units = StrategicUtilities.GetAllUnits();
            foreach (Unit unit in units)
            {
                unit.ReloadTraits();
            }
        }
        else
        {
            foreach (Actor_Unit actor in TacticalUtilities.Units)
            {
                actor.Unit.ReloadTraits();
                actor.Unit.InitializeTraits();
                actor.Unit.UpdateSpells();
            }
        }
        if (State.World.AllActiveEmpires != null)
        {
            foreach (Empire emp in State.World.AllActiveEmpires)
            {
                if (emp.Side > 300)
                    continue;
                var raceFlags = State.RaceSettings.GetRaceTraits(emp.ReplacedRace);
                if (raceFlags != null)
                {
                    if (raceFlags.Contains(Traits.Prey))
                        emp.CanVore = false;
                    else
                        emp.CanVore = true;
                }
            }
            foreach (Empire emp in State.World.MainEmpires)
            {
                if (emp.Side > 300)
                    continue;
                if (State.RaceSettings.Exists(emp.Race))
                {
                    emp.BannerType = State.RaceSettings.Get(emp.Race).BannerType;
                }
                else
                    emp.BannerType = 0;
            }
        }

    }

    public void CloseWithoutSave()
    {
        State.LoadEditedRaces();
        gameObject.SetActive(false);
    }

    public void ResetThisRace()
    {
        State.RaceSettings.Reset(PreviousRace);
        LoadRace();
        UpdateInteractable();
    }

    public void ResetAllRaces()
    {
        State.RaceSettings.ResetAll();
        LoadRace();
        UpdateInteractable();
    }



}
