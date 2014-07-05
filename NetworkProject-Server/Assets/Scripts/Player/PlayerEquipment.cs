using UnityEngine;
using System.Collections;
using NetworkProject;
using NetworkProject.BodyParts;

[System.CLSCompliant(false)]
public class PlayerEquipment : Equipment
{
    private PlayerEquipedItems _equipedItems = new PlayerEquipedItems();

    public EquipedItems GetEquipedItems()
    {
        return _equipedItems;
    }

    public Item GetEquipedItem(BodyPartSlot type)
    {
        return _equipedItems.GetEquipedItem(type);
    }

    public bool IsEmptySlot(BodyPartSlot bodyPart)
    {
        return _equipedItems.IsEmptySlot(bodyPart);
    }

    public void EquipeItem(Item item, BodyPartSlot bodyPartType)
    {
        _equipedItems.EquipeItem(item, bodyPartType);
    }

    public void SendUpdateAllSlots()
    {
        IConnectionMember address = GetComponent<NetPlayer>().Address;
        for(int i = 0; i < _items.Count; i++)
        {
            Server.SendMessageUpdateItemInEquipment(Server.ItemToItemInEquipmentPackage(_items[i]), i, address);
        }
        
    }

    public void SendUpdateSlot(int slot)
    {
        IConnectionMember address = GetComponent<NetPlayer>().Address;
        Server.SendMessageUpdateItemInEquipment(Server.ItemToItemInEquipmentPackage(_items[slot]), slot, address);
    }

    public bool CanEquipeItemOnThisBodyPart(ItemInfo item, BodyPartSlot bodyPart)
    {
        return _equipedItems.CanEquipeItemOnThisBodyPart(item, bodyPart);
    }
}
