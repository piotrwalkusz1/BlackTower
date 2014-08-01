using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NetworkProject;
using NetworkProject.Items;

public class ItemBag
{
    private List<Item> _items;

    public ItemBag()
    {
        _items = new List<Item>();

        for (int i = 0; i < Settings.widthEquipment * Settings.heightEquipment; i++)
        {
            _items.Add(null);
        }
    }

    public void SetItems(Item[] items)
    {
        _items.Clear();

        _items.AddRange(items);
    }

    public Item GetItem(int slot)
    {
        return _items[slot];
    }

    public Item[] GetItems()
    {
        return _items.ToArray();
    }

    public void UpdateSlot(int slotNumber, Item item)
    {
        _items[slotNumber] = item;
    }

    public bool IsSlotEmpty(int slot)
    {
        return _items[slot] == null;
    }
}
