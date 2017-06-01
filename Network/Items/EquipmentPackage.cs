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
    public class EquipmentPackage
    {
        public int Gold { get; set; }

        protected List<ItemPackage> _items;

        public EquipmentPackage()
        {
            _items = new List<ItemPackage>();
        }

        public EquipmentPackage(ItemPackage[] items, int gold)
        {
            _items = new List<ItemPackage>(items);

            Gold = gold;
        }

        public ItemPackage[] GetItems()
        {
            return _items.ToArray();
        }

        public ItemPackage GetItemBySlot(int slot)
        {
            return _items[slot];
        }

        public void ChangeItemInEquipment(int slot1, int slot2)
        {
            ItemPackage item1 = _items[slot1];

            _items[slot1] = _items[slot2];
            _items[slot2] = item1;
        }

        public int AddItem(ItemPackage item)
        {
            int index = FindFirstFreePlace();
            _items[index] = item;
            return index;
        }

        public void SetSlot(ItemPackage item, int slot)
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
