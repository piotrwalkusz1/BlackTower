using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject.Items;
using NetworkProject.Connection;
using NetworkProject.Connection.ToClient;

public class PlayerEquipment : MonoBehaviour
{
    private List<Item> _itemsInBag;

    public int Gold { get; set; }

    private List<BodyPart> _bodyParts;

    public void Set(EquipmentPackage itemBag, PlayerEquipedItemsPackage equipedItems)
    {
         _itemsInBag = PackageConverter.PackageToItem(itemBag.GetItems());

         Gold = itemBag.Gold;

        _bodyParts = PackageConverter.PackageToBodyPart(equipedItems.BodyParts.ToArray());
    }

    public void SetItemInBag(Item item, int idSlot)
    {
        _itemsInBag[idSlot] = item;
    }

    public Item GetItemFromBag(int slot)
    {
        try
        {
            return _itemsInBag[slot];
        }
        catch (IndexOutOfRangeException)
        {
            throw new ArgumentOutOfRangeException("Numer slotu ( " + slot.ToString() + ") jest poza zakresem");
        }
    }

    public Item[] GetItemsFromBag()
    {
        return _itemsInBag.ToArray();
    }

    public bool IsAnyEmptyBagSlot()
    {
        return _itemsInBag.Exists(x => x == null);
    }

    public bool AreEmptyBagSlots(int number)
    {
        int emptyBagSlotNumber = 0;

        foreach (var item in _itemsInBag)
        {
            if (item == null)
            {
                emptyBagSlotNumber++;
            }
        }

        return emptyBagSlotNumber >= number;
    }

    public bool IsBagSlotEmpty(int slot)
    {
        return _itemsInBag[slot] == null;
    }

    public void ChangeItemsInBag(int slot1, int slot2)
    {
        Item item1 = GetItemFromBag(slot1);
        Item item2 = GetItemFromBag(slot2);

        SetItemInBag(item1, slot2);
        SetItemInBag(item2, slot1);
    }

    public int AddItem(Item item)
    {
        int slot = GetEmptyBagSlot();

        SetItemInBag(item, slot);

        return slot;
    }

    public int AddItemAndSendUpdate(Item item)
    {
        int slot = AddItem(item);

        SendUpdateBagSlot(slot, GetComponent<NetPlayer>().OwnerAddress);

        return slot;
    }

    public Item GetEquipedItem(int slot)
    {
        return _bodyParts[slot].EquipedItem;
    }

    public BodyPart[] GetBodyParts()
    {
        return _bodyParts.ToArray();
    }

    public BodyPart GetBodyPart(int bodyPartSlot)
    {
        return _bodyParts[bodyPartSlot];
    }

    public bool CanEquipItem(Item item, PlayerStats stats)
    {
        EquipableItemData itemData = (EquipableItemData)item.ItemData;

        return itemData.CanEquipe(stats);
    }

    public bool CanEquipItemOnBodyPart(Item item, PlayerStats stats, BodyPart bodyPart)
    {
        return item == null || (CanEquipItem(item, stats) && bodyPart.CanEquipeItemOnThisBodyPart((EquipableItemData)item.ItemData));
    }

    public bool IsEmptyBodyPart(int bodyPart)
    {
        return GetEquipedItem(bodyPart) == null;
    }

    public bool IsEquipedWeapon()
    {
        foreach (var bodyPart in _bodyParts)
        {
            if (bodyPart.EquipedItem == null)
            {
                continue;
            }

            EquipableItemData item = (EquipableItemData)ItemRepository.GetItemByIdItem(bodyPart.EquipedItem.IdItem);

            if (item is WeaponData)
            {
                return true;
            }
        }

        return false;
    }

    public void SendUpdateBagSlot(int idSlot, IConnectionMember address)
    {
        int idNet = GetComponent<NetObject>().IdNet;

        Item item = GetItemFromBag(idSlot); 

        var request = new UpdateItemInEquipmentToClient(idNet, idSlot, PackageConverter.ItemToPackage(item));

        Server.SendRequestAsMessage(request, address);
    }

    public void SendUpdateGold(IConnectionMember address)
    {
        var request = new UpdateGoldToClient(GetComponent<NetObject>().IdNet, Gold);

        Server.SendRequestAsMessage(request, address);
    }

    public bool TryEquipItemFromBag(int bagSlot, int bodyPartSlot)
    {
        Item item = GetItemFromBag(bagSlot);

        BodyPart bodyPart = GetBodyPart(bodyPartSlot);

        Item oldItemOnBodyPart = bodyPart.EquipedItem;

        if(CanEquipItemOnBodyPart(item, GetComponent<PlayerStats>(), bodyPart))
        {
            bodyPart.EquipedItem = item;

            SetItemInBag(oldItemOnBodyPart, bagSlot);

            return true;           
        }
        else
        {
            return false;
        }
    }

    public bool TryChangeEquipedItems(int bodySlot1, int bodySlot2)
    {
        PlayerStats stats = GetComponent<PlayerStats>();

        var bodyPart1 = GetBodyPart(bodySlot1);
        var item1 = bodyPart1.EquipedItem;

        var bodyPart2 = GetBodyPart(bodySlot2);
        var item2 = bodyPart2.EquipedItem;

        if (CanEquipItemOnBodyPart(item1, stats, bodyPart2) && CanEquipItemOnBodyPart(item2, stats, bodyPart1))
        {
            bodyPart1.EquipedItem = item2;
            bodyPart2.EquipedItem = item1;

            return true;
        }
        else
        {
            return false;
        }
    }

    public void ApplyEquipedItemsToStats()
    {
        var stats = GetComponent<PlayerStats>();

        _bodyParts.ForEach(x => x.ApplyEquipedItemStats(stats));
        _bodyParts.ForEach(x => x.ApplyEquipedItemBenefits(stats));
    }

    public void UseItem(int slot)
    {
        Item item = GetItemFromBag(slot);

        if (item == null)
        {
            throw new ArgumentException("Nie ma itemu w tym slocie.");
        }
        
        try
        {
            var usableItem = (ItemUsableData)item.ItemData;

            usableItem.Use(gameObject);

            _itemsInBag[slot] = null;

            SendUpdateBagSlot(slot, GetComponent<NetPlayer>().OwnerAddress);
        }
        catch(InvalidCastException)
        {
            throw new ArgumentException("W tym slocie nie ma itemu zdolnego do użycia.");
        }
    }

    public int GetEmptyBagSlot()
    {
        for (int i = 0; i < _itemsInBag.Count; i++)
        {
            if (_itemsInBag[i] == null)
            {
                return i;
            }
        }

        return -1;
    }
}
