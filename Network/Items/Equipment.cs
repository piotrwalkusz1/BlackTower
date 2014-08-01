using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using NetworkProject;
using NetworkProject.Connection;
using NetworkProject.Connection.ToClient;

namespace NetworkProject.Items
{
    [Serializable]
    public class Equipment
    {
        protected List<Item> _items;

        public Equipment(int capacity)
        {
            _items = new List<Item>(new Item[capacity]);
        }

        public Item[] GetItems()
        {
            return _items.ToArray();
        }

        public Item GetItemBySlot(int slot)
        {
            return _items[slot];
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

        public void SetSlot(Item item, int slot)
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
}
