using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NetworkProject;
using NetworkProject.Items;
using NetworkProject.Connection;
using NetworkProject.Connection.ToClient;

[System.CLSCompliant(false)]
public class Equipment : MonoBehaviour
{
    protected List<Item> _items;

    void Awake()
    {
        _items = new List<Item>();

        for (int i = 0; i < Settings.widthEquipment * Settings.heightEquipment; i++)
        {
            _items.Add(null);
        }
    }

    public Item GetItemBySlot(int slot)
    {
        return _items[slot];
    }

    public void SendUpdateSlot(int slot, IConnectionMember address)
    {
        int myIdNet = GetComponent<NetObject>().IdNet;
        var item = GetItemBySlot(slot);

        var message = new UpdateItemInEquipment(myIdNet, slot, item);
    }

    public void ChangeItemInEquipment(int slot1, int slot2)
    {
        Item item1 = _items[slot1];

        _items[slot1] = _items[slot2];
        _items[slot2] = item1;
    }

    public int AddItem(Item item)
    {
        int index = FindFirstFreePlace();
        _items[index] = item;
        return index;
    }

    public void SetItem(Item item, int slot)
    {
        _items[slot] = item;
    }

    public void DeleteItme(int slot)
    {
        _items[slot] = null;
    }

    public bool IsFreePlace()
    {
        return (FindFirstFreePlace() != -1);
    }

    public bool IsThisPlaceFree(int numberPlace)
    {
        return _items[numberPlace] == null;
    }

    private int FindFirstFreePlace()
    {
        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i] == null)
            {
                return i;
            }
        }
        return -1;
    }
}
