using OdinSerializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class MercenaryContainer
{
    [OdinSerialize]
    internal Unit Unit;
    [OdinSerialize]
    internal int Cost;
    [OdinSerialize]
    internal string Title;
}

