using UnityEngine;
using System.Collections;
using NetworkProject;
using NetworkProject.BodyParts;
using NetworkProject.Items;
using NetworkProject.Connection;
using NetworkProject.Connection.ToClient;

public class PlayerEquipment : MonoBehaviour, IEquipmentManager
{
    private PlayerEquipedItems _equipedItems = new PlayerEquipedItems();

    private Equipment _equipment = new Equipment(NetworkProject.Settings.widthEquipment * NetworkProject.Settings.heightEquipment);

    public void Set(Equipment equipment, PlayerEquipedItems equipedItems)
    {
        _equipment = equipment;
        _equipedItems = equipedItems;
    }

    public PlayerEquipedItems GetEquipedItems()
    {
        return _equipedItems;
    }

    public Equipment GetEquipment()
    {
        return _equipment;
    }

    public Item GetEquipedItem(int slot)
    {
        return _equipedItems.GetEquipedItem(slot);
    }

    public Item GetItemInEquipment(int slot)
    {
        return _equipment.GetItemBySlot(slot);
    }

    public bool IsEmptySlotOnBody(int bodyPart)
    {
        return _equipedItems.IsEmptySlot(bodyPart);
    }

    public bool IsEmptySlotInEquipment(int slot)
    {
        return _equipment.IsThisPlaceFree(slot);
    }

    public bool IsAnyEmptySlotInEquipment()
    {
        return _equipment.IsFreePlace();
    }

    public int AddItem(Item item)
    {
        return _equipment.AddItem(item);
    }

    public void AddItemAndSendUpdate(Item item)
    {
        int slot = AddItem(item);

        var netPlayer = GetComponent<NetPlayer>();

        SendUpdateSlot(slot, netPlayer.OwnerAddress);
    }

    public bool CanEquipeItem(Item item, int slot)
    {
        if (item == null)
        {
            return true; // puste pole zawsze możne "założyć", innymi słowy jest to ściągnięcie itemu    
        }

        IEquipableItemManager itemDataManager = (IEquipableItemManager)ItemRepository.GetItemByIdItem(item.IdItem);
        EquipableItemData itemData = itemDataManager.GetEquipableItemData();
        BodyPart bodyPart = IoC.GetBodyPart(slot);

        return itemData.CanEquipe(GetComponent<PlayerStats>()) && bodyPart.CanEquipeItemOnThisBodyPart(itemData);
    }

    public void EquipeItem(Item item, int bodyPartType)
    {
        _equipedItems.EquipeItem(item, bodyPartType);
    }

    public bool CanEquipeItemInEquipmentOnThisBodyPart(int equipmentSlot, int bodyPartSlot)
    {
        Item itemInEquipment = _equipment.GetItemBySlot(equipmentSlot);

        return CanEquipeItem(itemInEquipment, bodyPartSlot);
    }

    public void EquipeItemInEquipmentOnThisBodyPart(int equipmentSlot, int bodyPartSlot)
    {
        Item itemInEquipment = _equipment.GetItemBySlot(equipmentSlot);
        Item equipedItem = GetEquipedItem(bodyPartSlot);

        EquipeItem(itemInEquipment, bodyPartSlot);
        _equipment.SetSlot(equipedItem, equipmentSlot);
    }

    public bool TryEquipeItemInEquipmentOnThisBodyPart(int equipmentSlot, int bodyPartSlot)
    {
        if (CanEquipeItemInEquipmentOnThisBodyPart(equipmentSlot, bodyPartSlot))
        {
            EquipeItemInEquipmentOnThisBodyPart(equipmentSlot, bodyPartSlot);
            return true;
        }
        else
        {
            return false;
        } 
    }

    public bool CanChangeEquipedItems(int slot1, int slot2)
    {
        Item item1 = GetEquipedItem(slot1);
        Item item2 = GetEquipedItem(slot2);

        return CanEquipeItem(item1, slot2) && CanEquipeItem(item2, slot1);
    }

    public void ChangeEquipedItems(int slot1, int slot2)
    {
        Item item1 = GetEquipedItem(slot1);
        Item item2 = GetEquipedItem(slot2);

        EquipeItem(item1, slot2);
        EquipeItem(item2, slot1);
    }

    public bool TryChangeEquipedItems(int slot1, int slot2)
    {
        if (CanChangeEquipedItems(slot1, slot2))
        {
            ChangeEquipedItems(slot1, slot2);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ApplyToStats(IEquipableStats stats)
    {
        _equipedItems.ApplyToStats(stats);
    }

    public bool IsEquipedWeapon()
    {
        return _equipedItems.IsEquipedWeapon();
    }

    public void SendUpdateSlot(int slot, IConnectionMember address)
    {
        var netObject = GetComponent<NetObject>();

        var request = new UpdateItemInEquipmentToClient(netObject.IdNet, slot, GetItemInEquipment(slot));

        Server.SendRequestAsMessage(request, address);
    }
}
