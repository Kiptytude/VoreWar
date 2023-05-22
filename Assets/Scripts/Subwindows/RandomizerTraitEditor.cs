using OdinSerializer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class RandomizerTraitEditor : MonoBehaviour
{
    public Transform Folder;
    public GameObject RandomizerTraitPrefab;
    internal List<RandomizerTrait> RandomizerTags;
    public Button AddBtn;
    internal Button AddBtnInstance;

    internal void Open()
    {
        gameObject.SetActive(true);

        int children = Folder.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(Folder.GetChild(i).gameObject);
        }

        Setup();
    }

    private void Setup()
    {
       RandomizerTags = new List<RandomizerTrait>();
        foreach (var entry in State.RandomizeLists)
        {
            var randomizerTrait = CreateRandomizerTrait(entry);
            RandomizerTags.Add(randomizerTrait);
        }
        CreateAddButton();
    }

    private void CreateAddButton()
    {
        if (AddBtnInstance != null)
        {
            AddBtnInstance.onClick.RemoveAllListeners();
            Destroy(AddBtnInstance.gameObject);
        }
        AddBtnInstance = Instantiate(AddBtn, Folder);
        var btn = AddBtnInstance.GetComponent<Button>();
        var btnTxt = btn.GetComponentInChildren<Text>();
        btnTxt.text = "Add";
        btn.onClick.AddListener(() =>
        {
            var created = CreateRandomizerTrait();
            RandomizerTags.Add(created);
            CreateAddButton();
        });
    }

    private RandomizerTrait CreateRandomizerTrait(RandomizeList savedCustom = null)
    {
        if (savedCustom != null)
        {
            var obj = Instantiate(RandomizerTraitPrefab, Folder);
            var rt = obj.GetComponent<RandomizerTrait>();
            rt.name.text = savedCustom.name;
            rt.chance.text = savedCustom.chance.ToString();
            rt.id = savedCustom.id;
            var ranTraits = new Dictionary<Traits, bool>();
            foreach (Traits trait in (Traits[])Enum.GetValues(typeof(Traits)))
            {
                if (savedCustom.RandomTraits.Contains(trait))
                    ranTraits[trait] = true;
                else
                    ranTraits[trait] = false;
            }
            rt.TraitDictionary = ranTraits;
            rt.CloneBtn.onClick.AddListener(() =>
            {
                var clone = CreateRandomizerTrait(savedCustom);
                clone.id = RandomizerTags.LastOrDefault().id + 1;
                clone.name.text = "new" + clone.name.text;
                RandomizerTags.Add(clone);
                CreateAddButton();
            });
            rt.RemoveBtn.onClick.AddListener(() =>
            {
                Remove(rt);
                Destroy(rt.gameObject);
            });
            return rt;
        }
        else
        {
            var newItemTemplate = Instantiate(RandomizerTraitPrefab, Folder);
            var rt = newItemTemplate.GetComponent<RandomizerTrait>();
            rt.name.text = "";
            rt.chance.text = "1.0";
            var last = RandomizerTags.LastOrDefault();
            rt.id = last == null ? 1001 : last.id + 1;
            var ranTraits = new Dictionary<Traits, bool>();
            foreach (Traits trait in (Traits[])Enum.GetValues(typeof(Traits)))
            {
                ranTraits[trait] = false;
            }
            rt.TraitDictionary = ranTraits;
            rt.CloneBtn.onClick.AddListener(() =>
            {
                var clone = CreateRandomizerTrait(savedCustom);
                clone.id = RandomizerTags.LastOrDefault().id + 1;
                clone.name.text = "new" + clone.name.text;
                RandomizerTags.Add(clone);
                CreateAddButton();
            });
            rt.RemoveBtn.onClick.AddListener(() =>
            {
                Remove(rt);
                Destroy(rt.gameObject);
            });
            return rt;
        }
    }

    private void Remove(RandomizerTrait rt)
    {
        foreach (Race race in (Race[])Enum.GetValues(typeof(Race)))
        {
            RaceSettingsItem item = State.RaceSettings.Get(race);
            item.RaceTraits.Remove((Traits)rt.id);
        }
        RandomizerTags.Remove(rt);
    }

    public void Persist()
    {
        List<RandomizeList> randomizeLists = new List<RandomizeList>();
        RandomizerTags.ForEach(tag =>
        {
            if (!Validate(tag))
            {
                State.GameManager.CreateMessageBox("Saving failed: Trait with name \"" + tag.name.text + "\" is incomplete or invalid.");
                return;
            }
            RandomizeList newCustom = new RandomizeList();
            newCustom.id = tag.id;
            newCustom.name = tag.name.text;
            newCustom.chance = float.Parse(tag.chance.text);
            newCustom.RandomTraits = new List<Traits>();
            foreach (var trait in tag.TraitDictionary)
            {
                if (trait.Value) newCustom.RandomTraits.Add(trait.Key);
            }
            randomizeLists.Add(newCustom);
        });
        State.RandomizeLists = randomizeLists;
        string[] printable = randomizeLists.ConvertAll(item => item.ToString()).ToArray();
        File.WriteAllLines($"{State.StorageDirectory}customTraits.txt", printable);
        Close();
    }

    public bool Validate(RandomizerTrait randomizerTrait)
    {
        float res;
        if (randomizerTrait.name.text.Length < 1) return false;
        if (randomizerTrait.chance.text.Length < 1 || !float.TryParse(randomizerTrait.chance.text, out res) || res < 0 || res > 1) return false;
        if (randomizerTrait.TraitDictionary.Where(i => i.Value).Count() < 1) return false;
        if (RandomizerTags.Where(rt => rt.name.text == randomizerTrait.name.text).Count() > 1) return false;
        return true;
    }

    public void Close()
    {
        gameObject.SetActive(false);
        int children = Folder.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(Folder.GetChild(i).gameObject);
        }
    }


}
