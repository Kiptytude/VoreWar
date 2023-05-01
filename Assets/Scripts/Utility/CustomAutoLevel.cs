using OdinSerializer;
using System.Collections.Generic;
using System.IO;


class StoredClassWeight
{
    [OdinSerialize]
    internal StatWeights Weights;
    [OdinSerialize]
    internal string Name;
}

static class CustomAutoLevel
{
    static CustomAutoLevel()
    {
        filename = $"{State.StorageDirectory}AutoLevels.cst";
        LoadData();
    }

    static string filename;
    static List<StoredClassWeight> weightsList;

    internal static string[] GetAllNames()
    {
        string[] names = new string[weightsList.Count];
        for (int i = 0; i < names.Length; i++)
        {
            names[i] = weightsList[i].Name;
        }
        return names;
    }


    static void LoadData()
    {
        if (File.Exists(filename))
        {
            byte[] bytes = File.ReadAllBytes(filename);
            weightsList = SerializationUtility.DeserializeValue<List<StoredClassWeight>>(bytes, DataFormat.JSON);
        }
        else
        {
            weightsList = new List<StoredClassWeight>();
        }
    }

    static void SaveData()
    {
        try
        {
            byte[] bytes = SerializationUtility.SerializeValue(weightsList, DataFormat.JSON);
            File.WriteAllBytes(filename, bytes);
        }
        catch
        {
            State.GameManager.CreateMessageBox("Couldn't save Custom Auto Levels to file for some reason");
        }

    }

    internal static StoredClassWeight GetByName(string name)
    {
        foreach (StoredClassWeight entry in weightsList)
        {
            if (entry.Name == name)
                return entry;
        }
        return null;
    }

    internal static void Remove(StoredClassWeight data)
    {
        weightsList.Remove(data);
        SaveData();
    }

    internal static void Add(StoredClassWeight data)
    {
        weightsList.Add(data);
        SaveData();
    }

}
