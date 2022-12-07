using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public enum StatusEffectType
{
    /// <summary>Reduces damage taken from attacks by 25%.</summary>
    Shielded,
    /// <summary>Heals up to 20% of max health per turn.</summary>
    Mending,
    /// <summary>Movement is increased.</summary>
    Fast,
    /// <summary>Melee damage is increased by 25%.</summary>
    Valor,
    /// <summary>Increases Voracity by 25% and Stomach by Strength.</summary>
    Predation,
    /// <summary>Deals damage over time.</summary>
    Poisoned,
    /// <summary>Doubles chance to be vored.</summary>
    WillingPrey,
    /// <summary>Scale and stats are increased by 20%.</summary>
    Enlarged,
    /// <summary>Scale and stats are reduced by 75%.</summary>
    Diminished,
    /// <summary>Dodge chance is reduced based on effect strength.</summary>
    Clumsiness,
    /// <summary>Stats are boosted based on effect strength and remaining duration.</summary>
    Empowered,
    /// <summary>Stats are reduced by effect strength.</summary>
    Shaken,
    /// <summary>Movement is reduced to 1 and stats are reduced by 30%.</summary>
    Webbed,
    /// <summary>Movement is reduced to 2.</summary>
    Glued,
    /// <summary>Strength is increased by 2 and movement by 1 per stack.</summary>
    BladeDance,
    /// <summary>Prevents movement and dodging, but halves damage taken from attacks and triples bulk as prey.</summary>
    Petrify,
    /// <summary>Strength, Agility, and Voracity are increased by 10% per remaining turn.</summary>
    Tenacious,
    /// <summary>Doubles Strength and Voracity.</summary>
    Berserk,
    /// <summary>Forces affected unit to be controlled by AI and act as opposing side.</summary>
    Charmed,
}

