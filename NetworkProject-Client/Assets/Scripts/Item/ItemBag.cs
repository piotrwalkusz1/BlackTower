using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NetworkProject;
using NetworkProject.Items;

public class ItemBag
{
    public int Gold { get; set; }

    private List<Item> _items;

    public ItemBag()
    {
        _items = new List<Item>();
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

    public bool IsEmptyAnyBagSlot()
    {
        return AreEmptyBagSlots(1);
    }

    public bool AreEmptyBagSlots(int number)
    {
        int emptyBagSlotNumber = 0;

        foreach (var item in _items)
        {
            if (item == null)
            {
                emptyBagSlotNumber++;
            }
        }

        return emptyBagSlotNumber >= number;
    }
}
