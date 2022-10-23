using UnityEngine;
using System.Collections;
using System.Reflection;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;
using System.Collections.Generic;

public class VariableEditor : MonoBehaviour
{
    object EditingObject;

    public Transform Folder;

    public GameObject Toggle;
    public GameObject InputField;
    public GameObject Slider;
    public GameObject Dropdown;

    public TextMeshProUGUI TooltipText;

    internal Dictionary<Traits, bool> TempDictionary;

    internal const BindingFlags Bindings = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

    internal void Open<T>(T obj)
    {
        if (obj == null)
        {
            Debug.Log("Tried to open on null object!");
            return;
        }

        gameObject.SetActive(true);
        EditingObject = obj;

        int children = Folder.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(Folder.GetChild(i).gameObject);
        }

        ProcessFields(obj);
        //ProcessProperties(obj);
    }

    private void ProcessFields<T>(T obj)
    {
        FieldInfo[] fields = obj.GetType().GetFields(Bindings);
        foreach (FieldInfo field in fields)
        {
            if (Attribute.GetCustomAttribute(field, typeof(AllowEditing)) == null)
                continue;
            if (field.FieldType == typeof(bool))
            {
                var newObj = Instantiate(Toggle, Folder);
                var toggle = newObj.GetComponent<Toggle>();
                newObj.name = field.Name;
                toggle.isOn = (bool)field.GetValue(obj);
                toggle.GetComponentInChildren<Text>().text = field.Name; //Designed to be overwritten by proper
                foreach (Attribute attr in Attribute.GetCustomAttributes(field))
                {
                    if (attr is ProperNameAttribute proper)
                    {
                        toggle.GetComponentInChildren<Text>().text = proper.Name;
                    }
                    if (attr is DescriptionAttribute desc)
                    {
                        toggle.gameObject.AddComponent<VariableScreenTooltip>();
                        toggle.GetComponent<VariableScreenTooltip>().text = desc.Description;
                    }
                }
            }
            if (field.FieldType == typeof(string))
            {
                var newObj = Instantiate(InputField, Folder);
                var input = newObj.GetComponent<CombinedInputfield>();
                newObj.name = field.Name;
                input.Inputfield.text = (string)field.GetValue(obj);
                newObj.GetComponent<CombinedInputfield>().Text.text = field.Name; //Designed to be overwritten by proper
                foreach (Attribute attr in Attribute.GetCustomAttributes(field))
                {
                    if (attr is ProperNameAttribute proper)
                    {
                        newObj.GetComponent<CombinedInputfield>().Text.text = proper.Name;
                    }
                    if (attr is DescriptionAttribute desc)
                    {
                        newObj.gameObject.AddComponent<VariableScreenTooltip>();
                        newObj.GetComponent<VariableScreenTooltip>().text = desc.Description;
                    }
                }
            }
            if (field.FieldType == typeof(int))
            {
                var newObj = Instantiate(InputField, Folder);
                var input = newObj.GetComponent<CombinedInputfield>();
                newObj.name = field.Name;
                input.Inputfield.text = field.GetValue(obj).ToString();
                input.Inputfield.characterValidation = TMP_InputField.CharacterValidation.Integer;
                input.Inputfield.characterLimit = 5;
                newObj.GetComponent<CombinedInputfield>().Text.text = field.Name; //Designed to be overwritten by proper
                foreach (Attribute attr in Attribute.GetCustomAttributes(field))
                {
                    if (attr is ProperNameAttribute proper)
                    {
                        newObj.GetComponent<CombinedInputfield>().Text.text = proper.Name;
                    }
                    if (attr is DescriptionAttribute desc)
                    {
                        newObj.gameObject.AddComponent<VariableScreenTooltip>();
                        newObj.GetComponent<VariableScreenTooltip>().text = desc.Description;
                    }
                }
            }
            if (field.FieldType == typeof(float))
            {
                var newObj = Instantiate(Slider, Folder);
                var slider = newObj.GetComponentInChildren<Slider>();
                newObj.name = field.Name;
                newObj.GetComponentInChildren<TextMeshProUGUI>().text = field.Name; //Designed to be overwritten by proper
                foreach (Attribute attr in Attribute.GetCustomAttributes(field))
                {
                    if (attr is ProperNameAttribute proper)
                    {
                        newObj.GetComponentInChildren<TextMeshProUGUI>().text = proper.Name;
                    }
                    if (attr is DescriptionAttribute desc)
                    {
                        newObj.gameObject.AddComponent<VariableScreenTooltip>();
                        newObj.GetComponent<VariableScreenTooltip>().text = desc.Description;
                    }
                    if (attr is FloatRangeAttribute range)
                    {
                        slider.minValue = range.Min;
                        slider.maxValue = range.Max;
                    }
                }
                slider.value = (float)field.GetValue(obj); // Must be set after the min and max are set
            }
            if (field.FieldType.BaseType == typeof(Enum))
            {
                var newObj = Instantiate(Dropdown, Folder);
                var dropdown = newObj.GetComponentInChildren<TMP_Dropdown>();
                newObj.name = field.Name;
                Type enumType = field.FieldType;
                var values = Enum.GetValues(enumType);
                dropdown.ClearOptions();
                for (int i = 0; i < values.Length; i++)
                {
                    dropdown.options.Add(new TMP_Dropdown.OptionData(values.GetValue(i).ToString()));
                }
                dropdown.RefreshShownValue();
                dropdown.value = (int)field.GetValue(obj);
                newObj.GetComponentInChildren<TextMeshProUGUI>().text = field.Name; //Designed to be overwritten by proper
                foreach (Attribute attr in Attribute.GetCustomAttributes(field))
                {
                    if (attr is ProperNameAttribute proper)
                    {
                        newObj.GetComponentInChildren<TextMeshProUGUI>().text = proper.Name;
                    }
                    if (attr is DescriptionAttribute desc)
                    {
                        newObj.gameObject.AddComponent<VariableScreenTooltip>();
                        newObj.GetComponent<VariableScreenTooltip>().text = desc.Description;
                    }
                }
            }

            if (field.FieldType == typeof(Dictionary<Traits, bool>))
            {
                TempDictionary = (Dictionary<Traits, bool>)field.GetValue(obj);
                if (TempDictionary != null)
                {
                    foreach (var entry in TempDictionary.OrderBy(s =>
                       {
                           return s.Key >= Traits.LightningSpeed ? "ZZZ" + s.ToString() : s.ToString();
                       }))
                    {
                        var newObj = Instantiate(Toggle, Folder);
                        var toggle = newObj.GetComponent<Toggle>();
                        newObj.name = $"UsingDictionary^{entry.Key}";
                        toggle.isOn = entry.Value;
                        toggle.GetComponentInChildren<Text>().text = entry.Key.ToString();
                    }
                }
                //var newObj = Instantiate(Toggle, Folder);
                //var toggle = newObj.GetComponent<Toggle>();
                //newObj.name = field.Name;
                //toggle.isOn = (bool)field.GetValue(obj);
                //toggle.GetComponentInChildren<Text>().text = field.Name; //Designed to be overwritten by proper
                //foreach (Attribute attr in Attribute.GetCustomAttributes(field))
                //{
                //    if (attr is ProperNameAttribute proper)
                //    {
                //        toggle.GetComponentInChildren<Text>().text = proper.Name;
                //    }
                //    if (attr is DescriptionAttribute desc)
                //    {
                //        toggle.gameObject.AddComponent<VariableScreenTooltip>();
                //        toggle.GetComponent<VariableScreenTooltip>().text = desc.Description;
                //    }
                //}
            }


        }
    }

    //private void ProcessProperties<T>(T obj)
    //{
    //    PropertyInfo[] properties = obj.GetType().GetProperties(Bindings);
    //    foreach (PropertyInfo property in properties)
    //    {
    //        if (property.PropertyType == typeof(bool))
    //        {
    //            var newObj = Instantiate(Toggle, Folder);
    //            var toggle = newObj.GetComponent<Toggle>();
    //            newObj.name = property.Name;
    //            toggle.isOn = (bool)property.GetValue(obj);
    //            toggle.GetComponentInChildren<TextMeshProUGUI>().text = property.Name; //Designed to be overwritten by proper
    //            foreach (Attribute attr in Attribute.GetCustomAttributes(property))
    //            {
    //                if (attr is ProperNameAttribute proper)
    //                {
    //                    toggle.GetComponentInChildren<TextMeshProUGUI>().text = proper.Name;
    //                }
    //                if (attr is DescriptionAttribute desc)
    //                {
    //                    toggle.gameObject.AddComponent<VariableScreenTooltip>();
    //                    toggle.GetComponent<VariableScreenTooltip>().text = desc.Description;
    //                }
    //            }
    //        }
    //        if (property.PropertyType == typeof(string))
    //        {
    //            var newObj = Instantiate(InputField, Folder);
    //            var input = newObj.GetComponent<CombinedInputfield>();
    //            newObj.name = property.Name;
    //            input.Inputfield.text = (string)property.GetValue(obj);
    //            newObj.GetComponent<CombinedInputfield>().Text.text = property.Name; //Designed to be overwritten by proper
    //            foreach (Attribute attr in Attribute.GetCustomAttributes(property))
    //            {
    //                if (attr is ProperNameAttribute proper)
    //                {
    //                    newObj.GetComponent<CombinedInputfield>().Text.text = proper.Name;
    //                }
    //                if (attr is DescriptionAttribute desc)
    //                {
    //                    newObj.gameObject.AddComponent<VariableScreenTooltip>();
    //                    newObj.GetComponent<VariableScreenTooltip>().text = desc.Description;
    //                }
    //            }
    //        }
    //        //if (field.FieldType == typeof(int))
    //        //{
    //        //    var newObj = Instantiate(InputField, Folder);
    //        //    var input = newObj.GetComponent<CombinedInputfield>();
    //        //    newObj.name = field.Name;
    //        //    input.Inputfield.text = ((int)field.GetValue(obj)).ToString();
    //        //    foreach (Attribute attr in Attribute.GetCustomAttributes(field))
    //        //    {
    //        //        if (attr is ProperNameAttribute proper)
    //        //        {
    //        //            newObj.GetComponentInChildren<TextMeshProUGUI>().text = proper.Name;
    //        //        }
    //        //        if (attr is DescriptionAttribute desc)
    //        //        {
    //        //            newObj.gameObject.AddComponent<VariableScreenTooltip>();
    //        //            newObj.GetComponent<VariableScreenTooltip>().text = desc.Description;
    //        //        }
    //        //    }
    //        //}
    //        if (property.PropertyType == typeof(float))
    //        {
    //            var newObj = Instantiate(Slider, Folder);
    //            var slider = newObj.GetComponentInChildren<Slider>();
    //            newObj.name = property.Name;
    //            newObj.GetComponentInChildren<TextMeshProUGUI>().text = property.Name; //Designed to be overwritten by proper
    //            foreach (Attribute attr in Attribute.GetCustomAttributes(property))
    //            {
    //                if (attr is ProperNameAttribute proper)
    //                {
    //                    newObj.GetComponentInChildren<TextMeshProUGUI>().text = proper.Name;
    //                }
    //                if (attr is DescriptionAttribute desc)
    //                {
    //                    newObj.gameObject.AddComponent<VariableScreenTooltip>();
    //                    newObj.GetComponent<VariableScreenTooltip>().text = desc.Description;
    //                }
    //                if (attr is FloatRangeAttribute range)
    //                {
    //                    slider.minValue = range.Min;
    //                    slider.maxValue = range.Max;
    //                }
    //            }
    //            slider.value = (float)property.GetValue(obj); // Must be set after the min and max are set
    //        }
    //        if (property.PropertyType.BaseType == typeof(Enum))
    //        {
    //            var newObj = Instantiate(Dropdown, Folder);
    //            var dropdown = newObj.GetComponentInChildren<TMP_Dropdown>();
    //            newObj.name = property.Name;
    //            Type enumType = property.PropertyType;
    //            var values = Enum.GetValues(enumType);
    //            dropdown.ClearOptions();
    //            for (int i = 0; i < values.Length; i++)
    //            {
    //                dropdown.options.Add(new TMP_Dropdown.OptionData(values.GetValue(i).ToString()));
    //            }
    //            dropdown.RefreshShownValue();
    //            dropdown.value = (int)property.GetValue(obj);
    //            newObj.GetComponentInChildren<TextMeshProUGUI>().text = property.Name; //Designed to be overwritten by proper
    //            foreach (Attribute attr in Attribute.GetCustomAttributes(property))
    //            {
    //                if (attr is ProperNameAttribute proper)
    //                {
    //                    newObj.GetComponentInChildren<TextMeshProUGUI>().text = proper.Name;
    //                }
    //                if (attr is DescriptionAttribute desc)
    //                {
    //                    newObj.gameObject.AddComponent<VariableScreenTooltip>();
    //                    newObj.GetComponent<VariableScreenTooltip>().text = desc.Description;
    //                }
    //            }
    //        }
    //    }
    //}

    internal void ChangeToolTip(string text)
    {
        TooltipText.text = text;
    }

    public void SaveAndClose()
    {
        bool needSave = false;
        int children = Folder.childCount;
        for (int i = 0; i < children; i++)
        {
            var obj = Folder.GetChild(i).gameObject;
            var drop = obj.GetComponentInChildren<TMP_Dropdown>();
            if (drop != null)
            {
                EditingObject.GetType().GetField(obj.name, Bindings)?.SetValue(EditingObject, drop.value);
                continue;
            }
            var toggle = obj.GetComponentInChildren<Toggle>();
            if (toggle != null)
            {
                EditingObject.GetType().GetField(obj.name, Bindings)?.SetValue(EditingObject, toggle.isOn);
                if (obj.name.Contains("UsingDictionary"))
                {
                    var split = obj.name.Split('^');
                    if (Enum.TryParse(split[1], out Traits trait))
                    {
                        TempDictionary[trait] = obj.GetComponentInChildren<Toggle>().isOn;
                    }
                    needSave = true;
                }
                continue;
            }
            var slider = obj.GetComponentInChildren<Slider>();
            if (slider != null)
            {
                EditingObject.GetType().GetField(obj.name, Bindings)?.SetValue(EditingObject, slider.value);
                continue;
            }
            var input = obj.GetComponentInChildren<TMP_InputField>();
            if (input != null)
            {               
                if (int.TryParse(input.text, out int result))
                {
                    var attr = EditingObject.GetType().GetField(obj.name)?.GetCustomAttribute(typeof(IntegerRangeAttribute));
                    if (attr != null)
                    {
                        var range = (IntegerRangeAttribute)attr;
                        result = Mathf.Clamp(result, range.Min, range.Max);
                    }

                    EditingObject.GetType().GetField(obj.name, Bindings)?.SetValue(EditingObject, result);
                }

                continue;
            }
            Debug.LogWarning("Couldn't handle object!");
        }
        gameObject.SetActive(false);
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(Folder.GetChild(i).gameObject);
        }
        if (State.GameManager.Menu.WorldSettingsUI.gameObject.activeSelf)
        {
            State.GameManager.Menu.WorldSettingsUI.ShowSettings();
        }
        if (needSave)
            State.AssimilateList.Save();
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
