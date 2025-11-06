using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

public class UnitTag
{
    public int id = -1;
    public string name;
    public List<UnitTagModifier> modifiers;
    [AllowEditing]
    public List<Traits> AssociatedTraits;
}