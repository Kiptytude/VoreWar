
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class EnumIndexedArray<TEnum, T>
{
    private readonly T[] _data = new T[Enum.GetNames(typeof(TEnum)).Length];

    /// <summary>
    /// Returns null if value is not set
    /// </summary>
    /// <param name="index"></param>
    internal T this[TEnum index]
    {
        get => _data[(int)(object)index];
        set => _data[(int)(object)index] = value;
    }
}
