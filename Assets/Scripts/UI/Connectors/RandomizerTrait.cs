using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RandomizerTrait : MonoBehaviour
{
    internal int id;
    public TraitListType listType;
    public InputField name;
    public InputField chance;
    public InputField level;
    public Toggle permanent;
    public Button PickTagsBtn;
    public Button ExpandBtn;
    public Button CloneBtn;
    public Button RemoveBtn;


    [AllowEditing]
    internal Dictionary<Traits, bool> TraitDictionary;

    public void OpenTraitsDict()
    {
        State.GameManager.VariableEditor.Open(this);
    }

}