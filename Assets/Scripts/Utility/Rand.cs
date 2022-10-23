using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Rand
{
    Random rand = new Random();

    internal int Next()
    {
        return rand.Next();
    }
       

    internal int Next(int maxValue)
    {
        if (maxValue < 1)
            maxValue = 1;
        return rand.Next(maxValue);
    }

    internal int Next(int minValue, int maxValue)
    {
        if (maxValue < minValue)
            maxValue = minValue;
        if (maxValue < 1)
            maxValue = 1;
        return rand.Next(minValue, maxValue);
    }

    internal double NextDouble()
    {
        return rand.NextDouble();
    }
}

