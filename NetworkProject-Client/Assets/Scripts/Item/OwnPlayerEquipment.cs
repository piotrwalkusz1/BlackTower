using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject.Items;
using NetworkProject.Connection.ToServer;

public class OwnPlayerEquipment : PlayerEquipment, IEquipment
{
    public int Gold
    {
        get { return _equipment.Gold; }
        set { _equipment.Gold = value; }
    }

    private ItemBag _equipment;

    public OwnPlayerEquipment()
    {
        _equipment = new ItemBag();
    }

    public void SetEquipmentBag(EquipmentPackage package)
    {
        _equipment.SetItems(PackageConverter.PackageToItem(package.GetItems()).ToArray());

        _equipment.Gold = package.Gold;
    }

    public void SetSlot(Item item, int idSlot)
    {
        _equipment.UpdateSlot(idSlot, item);

        GUIController.IfActiveEquipmentRefresh();
    }

    public Item GetItemFromBag(int idSlot)
    {
        return _equipment.GetItem(idSlot);
    }

    public Item[] GetItemsFromBag()
    {
        return _equipment.GetItems();
    }

    public override void Equip(Item item, int bodyPart)
    {
        base.Equip(item, bodyPart);

        GUIController.IfActiveEquipmentRefresh();
    }

    public bool IsEmptyBagSlot(int idSlot)
    {
        return GetItemFromBag(idSlot) == null;
    }

    public bool IsEmptyAnyBagSlot()
    {
        return _equipment.IsEmptyAnyBagSlot();
    }

    public Item GetSlot(int idSlot)
    {
        return _equipment.GetItem(idSlot);
    }

    public bool CanTakeRewardForQuest(int idQuest)
    {
        Quest quest = QuestRepository.GetQuest(idQuest);

        var itemRewards = from reward in quest.GetRewards()
                          where reward is ItemReward
                          select reward;

        return _equipment.AreEmptyBagSlots(itemRewards.ToArray().Length);
    }

    public bool TryEquipItemFromBagAndSendMessage(int slotInEquipment, int slotInBodyParts)
    {
        var item = GetItemFromBag(slotInEquipment);
        var itemData = item == null ? null : (EquipableItemData)item.ItemData;

        var bodyPart = GetBodyPart(slotInBodyParts);

        if (CanEquip(itemData, bodyPart))
        {
            var request = new ChangeEquipedItemToServer(slotInEquipment, slotInBodyParts);

            Client.SendRequestAsMessage(request);

            return true;
        }
        else
        {
            return false;
        }
    }

    public bool TryChangeEquipedItemsAndSendMessage(int slot1, int slot2)
    {
        var bodyPart1 = GetBodyPart(slot1);
        var item1 = bodyPart1.EquipedItem == null ? null : (EquipableItemData)bodyPart1.EquipedItem.ItemData;

        var bodyPart2 = GetBodyPart(slot2);
        var item2 = bodyPart2.EquipedItem == null ? null : (EquipableItemData)bodyPart2.EquipedItem.ItemData;

        if (CanEquip(item1, bodyPart2) && CanEquip(item2, bodyPart1))
        {
            var request = new ChangeEquipedItemsToServer(slot1, slot2);

            Client.SendRequestAsMessage(request);

            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CanEquip(EquipableItemData item, BodyPart bodyPart)
    {
        if (item == null)
        {
            return true;
        }

        PlayerStats stats = GetComponent<PlayerStats>();

        return item.CanEquip(stats) && bodyPart.CanEquipeItemOnThisBodyPart(item);
    }

    public int FindBodyPartForItem(EquipableItemData item)
    {
        var matchedBodyParts = from bodyPart in GetBodyParts()
                               where bodyPart.CanEquipeItemOnThisBodyPart(item)
                               select bodyPart;

        var bodyParts = matchedBodyParts.ToList();

        var emptyBodyPart = bodyParts.FirstOrDefault(x => x.EquipedItem == null);

        if (emptyBodyPart == null)
        {
            return GetBodyParts().ToList().IndexOf(bodyParts[0]);
        }
        else
        {
            return GetBodyParts().ToList().IndexOf(emptyBodyPart);
        }
    }
}
