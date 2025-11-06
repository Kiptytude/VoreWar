using OdinSerializer;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


class ItemStock
{
    [OdinSerialize]
    Dictionary<ItemType, int> Items = new Dictionary<ItemType, int>();

    internal void AddItem(ItemType type, int quantity = 1)
    {
        if (Items == null)
            Items = new Dictionary<ItemType, int>();
        if (Items.ContainsKey(type))
        {
            Items[type] += quantity;
        }
        else
            Items[type] = quantity;
    }

    internal bool HasItem(ItemType type)
    {
        if (Items == null)
            Items = new Dictionary<ItemType, int>();
        if (Items.TryGetValue(type, out int num))
        {
            return num > 0;
        }
        return false;
    }

    internal int ItemCount(ItemType type)
    {
        if (Items == null)
            Items = new Dictionary<ItemType, int>();
        if (Items.TryGetValue(type, out int num))
        {
            return num;
        }
        return 0;
    }

    internal bool TakeItem(ItemType type)
    {
        if (Items == null)
            Items = new Dictionary<ItemType, int>();
        if (Items.TryGetValue(type, out int num))
        {
            if (num > 0)
            {
                Items[type]--;
                return true;
            }
        }
        return false;
    }

    internal List<ItemType> GetAllSpellBooks()
    {
        if (Items == null)
            Items = new Dictionary<ItemType, int>();
        List<ItemType> items = new List<ItemType>();
        foreach (var item in Items)
        {
            if (item.Key >= ItemType.Meditate && item.Key <= ItemType.GateMaw)
            {
                if (item.Value > 0)
                {
                    for (int i = 0; i < item.Value; i++)
                    {
                        items.Add(item.Key);
                    }
                }
            }
        }
        return items;
    }

    internal List<ItemType> GetAllPotions()
    {
        if (Items == null)
            Items = new Dictionary<ItemType, int>();
        List<ItemType> items = new List<ItemType>();
        foreach (var item in Items)
        {
            if (item.Key >= ItemType.HealthPotion && item.Key <= ItemType.OmniPotion)
            {
                if (item.Value > 0)
                {
                    for (int i = 0; i < item.Value; i++)
                    {
                        items.Add(item.Key);
                    }
                }
            }
        }
        return items;
    }
    internal List<ItemType> SellAllWeaponsAndAccessories(Empire empire)
    {
        if (Items == null)
            Items = new Dictionary<ItemType, int>();
        List<ItemType> items = new List<ItemType>();
        foreach (var item in Items)
        {
            if (item.Key < ItemType.Mace)
            {
                continue;
            }
            if (item.Key < ItemType.HealthPotion)
            {

                if (item.Value > 0)
                {
                    empire.AddGold(State.World.ItemRepository.GetItem(item.Key).Cost / 2 * item.Value);
                }
            }
        }
        return items;
    }

    internal bool TransferAllItems(ItemStock destination)
    {
        if (Items == null)
            Items = new Dictionary<ItemType, int>();
        bool foundItem = false;
        foreach (var item in Items.ToList())
        {
            if (item.Key < ItemType.Mace)
            {
                continue;
            }
            if (item.Value > 0)
            {
                foundItem = true;
                destination.AddItem(item.Key, item.Value);
                Items[item.Key] = 0;
            }
        }
        return foundItem;
    }




    internal bool TransferAllItems(ItemStock destination, ref List<Item> foundItems)
    {
        if (Items == null)
            Items = new Dictionary<ItemType, int>();
        bool foundItem = false;
        foreach (var item in Items.ToList())
        {
            if (item.Key < ItemType.Mace)
            {
                continue;
            }
            if (item.Value > 0)
            {
                foundItem = true;
                foundItems.Add(State.World.ItemRepository.GetItem(item.Key));
                destination.AddItem(item.Key, item.Value);
                Items[item.Key] = 0;
            }
        }
        return foundItem;
    }


}

