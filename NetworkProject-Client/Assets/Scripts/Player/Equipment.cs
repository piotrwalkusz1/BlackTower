using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NetworkProject;
using NetworkProject.Items;

public class Equipment
{
    private List<Item> _items;

    public Equipment()
    {
        _items = new List<Item>();

        for (int i = 0; i < Settings.widthEquipment * Settings.heightEquipment; i++)
        {
            _items.Add(null);
        }
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
        if (item.IdItem == -1)
        {
            _items[slotNumber] = null;
        }
        else
        {
            _items[slotNumber] = item;
        }  
    }

    public bool IsSlotEmpty(int slot)
    {
        return _items[slot] == null;
    }
}
