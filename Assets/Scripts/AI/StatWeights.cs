using OdinSerializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class StatWeights
{
    [OdinSerialize]
    internal float[] Weight = new float[(int)Stat.None];
}

