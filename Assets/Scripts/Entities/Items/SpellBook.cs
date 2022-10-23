using OdinSerializer;

public class SpellBook : Item
{
    public SpellBook(string name, string description, int cost, int tier, SpellTypes containedSpell)
    {
        Name = name;
        Description = description;
        Cost = cost;
        Tier = tier;
        ContainedSpell = containedSpell;
    }

    [OdinSerialize]
    internal SpellTypes ContainedSpell { get; private set; }
    [OdinSerialize]
    internal int Tier { get; private set; }

    public string DetailedDescription()
    {
        if (SpellList.SpellDict.TryGetValue(ContainedSpell, out Spell spell))
        {
            return $"{spell.Description}\nRange: {spell.Range.Min}-{spell.Range.Max}\nMana Cost: {spell.ManaCost}\nTargets: {string.Join(", ", spell.AcceptibleTargets)}";
        }
        else
            return $"Error reading spell";
    }
}
