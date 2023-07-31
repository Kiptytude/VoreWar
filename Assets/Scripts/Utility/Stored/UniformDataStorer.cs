using OdinSerializer;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class UniformObject
{
    [OdinSerialize]
    internal Dictionary<Race, List<UniformData>> Uniforms;
    [OdinSerialize]
    internal Dictionary<Race, float> Odds;
}

static class UniformDataStorer
{
    static string filename;
    static UniformObject Data;

    internal static float GetUniformOdds(Race race)
    {
        if (Data.Odds.TryGetValue(race, out var value))
        {
            return value;
        }
        else
        {
            return 1;
        }
    }

    internal static void SetUniformOdds(Race race, float value)
    {
        Data.Odds[race] = value;
        SaveData();
    }

    static UniformDataStorer()
    {
        filename = $"{State.StorageDirectory}UniformData.cst";
        LoadData();
    }

    static void LoadData()
    {
        if (File.Exists(filename))
        {
            byte[] bytes = File.ReadAllBytes(filename);
            Data = SerializationUtility.DeserializeValue<UniformObject>(bytes, DataFormat.Binary);
        }
        else
        {
            Data = new UniformObject
            {
                Uniforms = new Dictionary<Race, List<UniformData>>(),
                Odds = new Dictionary<Race, float>()
            };
        }
    }

    static void SaveData()
    {
        byte[] bytes = SerializationUtility.SerializeValue(Data, DataFormat.Binary);
        File.WriteAllBytes(filename, bytes);
    }

    internal static void Remove(UniformData unitCustomizer)
    {
        if (Data.Uniforms.TryGetValue(unitCustomizer.Race, out var value))
        {
            value.Remove(unitCustomizer);
            SaveData();
        }

    }

    internal static void Add(UniformData unitCustomizer)
    {
        if (Data.Uniforms.TryGetValue(unitCustomizer.Race, out var value))
        {
            var replace = value.Where(s => s.Name == unitCustomizer.Name).FirstOrDefault();
            if (replace != null)
                value.Remove(replace);
            value.Add(unitCustomizer);
        }
        else
        {
            var newList = new List<UniformData>();
            newList.Add(unitCustomizer);
            Data.Uniforms[unitCustomizer.Race] = newList;
        }

        SaveData();
    }

    static internal void ExternalCopyToUnit(UniformData data, Unit unit)
    {
        data.CopyToUnit(unit);
    }

    static internal List<UniformData> GetCompatibleCustomizations(Unit unit)
    {
        return GetCompatibleCustomizations(unit.Race, unit.Type, unit.GetGender());
    }

    static internal List<UniformData> GetCompatibleCustomizations(Race race, UnitType type, Gender gender)
    {
        if (Data.Uniforms.TryGetValue(race, out var values))
        {
            if (type == UnitType.Leader)
                return values.Where(s => s.Type == type && GenderOkay(s.Gender, gender)).ToList();;
            return values.Where(s => GenderOkay(s.Gender, gender) && (s.Type == type || s.Type != UnitType.Leader)).ToList();
        }
        return null;

        bool GenderOkay(Gender person, Gender uniform)
        {
            if (person == Gender.Male && uniform == Gender.Male)
                return true;
            if (person != Gender.Male && uniform != Gender.Male)
                return true;
            return false;
        }

    }

    static internal List<UniformData> GetIncompatibleCustomizations(Unit unit)
    {
        return GetIncompatibleCustomizations(unit.Race, unit.Type, unit.GetGender());
    }

    static internal List<UniformData> GetIncompatibleCustomizations(Race race, UnitType type, Gender gender)
    {
        if (Data.Uniforms.TryGetValue(race, out var values))
        {
            if (type == UnitType.Leader)
                return values.Where(s => !(s.Type == type && GenderOkay(s.Gender, gender))).ToList();   
            return values.Where(s => !(GenderOkay(s.Gender, gender) && (s.Type == type || s.Type != UnitType.Leader))).ToList();
        }
        return null;

        bool GenderOkay(Gender person, Gender uniform)
        {
            if (person == Gender.Male && uniform == Gender.Male)
                return true;
            if (person != Gender.Male && uniform != Gender.Male)
                return true;
            return false;
        }

    }



}

