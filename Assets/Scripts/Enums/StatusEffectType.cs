public enum StatusEffectType
{
    /// <summary>Reduces damage taken from attacks by 25%.</summary>
    Shielded = 0,
    /// <summary>Heals up to 20% of max health per turn.</summary>
    Mending = 1,
    /// <summary>Movement is increased.</summary>
    Fast = 2,
    /// <summary>Melee damage is increased by 25%.</summary>
    Valor = 3,
    /// <summary>Increases Voracity by 25% and Stomach by Strength.</summary>
    Predation = 4,
    /// <summary>Deals damage over time.</summary>
    Poisoned = 5,
    /// <summary>Doubles chance to be vored.</summary>
    WillingPrey = 6,
    /// <summary>Scale and stats are increased by 20%.</summary>
    Enlarged = 7,
    /// <summary>Scale and stats are reduced by 75%.</summary>
    Diminished = 8,
    /// <summary>Dodge chance is reduced based on effect strength.</summary>
    Clumsiness = 9,
    /// <summary>Stats are boosted based on effect strength and remaining duration.</summary>
    Empowered = 10,
    /// <summary>Stats are reduced by effect strength.</summary>
    Shaken = 11,
    /// <summary>Movement is reduced to 1 and stats are reduced by 30%.</summary>
    Webbed = 12,
    /// <summary>Movement is reduced to 2.</summary>
    Glued = 13,
    /// <summary>Strength is increased by 2 and movement by 1 per stack.</summary>
    BladeDance = 14,
    /// <summary>Prevents movement and dodging, but halves damage taken from attacks and triples bulk as prey.</summary>
    Petrify = 15,
    /// <summary>Strength, Agility, and Voracity are increased by 10% per remaining turn.</summary>
    Tenacious = 16,
    /// <summary>Doubles Strength and Voracity.</summary>
    Berserk = 17,
    /// <summary>Forces affected unit to be controlled by AI and act as the caster's side.</summary>
    Charmed = 18,
    /// <summary>Affected units are unable to do anything but servicing the caster's side, who can also swallow them as easily as surrendered units</summary>
    Hypnotized = 19,
    /// <summary>Corruption buildup. Hidden to the afflicted and their allies. Once the buildup reaches the unit's stat total, it will gain the Corrupted trait and become controlled by the corruption's origin</summary>
    Corruption = 20,
}

