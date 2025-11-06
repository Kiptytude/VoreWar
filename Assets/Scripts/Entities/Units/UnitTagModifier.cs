using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

public class UnitTagModifier
{
    public int id = -1;
    public UnitTagModifierEffect tagEffect = 0;
    public UnitTagModifierCase tagCase = 0;
    public UnitTagModifierTarget target = 0;
    public float modifierValue = 0;
    public int targetValue = 0;
}