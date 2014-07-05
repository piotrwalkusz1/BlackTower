using UnityEngine;
using System.Collections;
using NetworkProject;
using NetworkProject.BodyParts;
using NetworkProject.Items;
using NetworkProject.Connection;

[System.CLSCompliant(false)]
public class PlayerEquipment : Equipment
{
    private PlayerEquipedItems _equipedItems = new PlayerEquipedItems();

    public EquipedItems GetEquipedItems()
    {
        return _equipedItems;
    }

    public Item GetEquipedItem(BodyPartSlot slot)
    {
        return _equipedItems.GetEquipedItem(slot);
    }

    public bool IsEmptySlot(BodyPartSlot bodyPart)
    {
        return _equipedItems.IsEmptySlot(bodyPart);
    }

    public bool CanEquipeItem(Item item, BodyPartSlot slot)
    {
        if (item == null)
        {
            return true; // puste pole zawsze możne "założyć", inaczej jest to ściągnięcie itemu    
        }

        EquipableItemData itemData = (EquipableItemData)ItemRepository.GetItemByIdItem(item.IdItem);
        BodyPart bodyPart = IoC.GetBodyPart(slot);

        return itemData.CanEquipe(GetComponent<PlayerStats>()) && bodyPart.CanEquipeItemOnThisBodyPart(itemData);
    }

    public void EquipeItem(Item item, BodyPartSlot bodyPartType)
    {
        _equipedItems.EquipeItem(item, bodyPartType);
    }

    public bool CanEquipeItemInEquipmentOnThisBodyPart(int equipmentSlot, BodyPartSlot bodyPartSlot)
    {
        Item itemInEquipment = GetItemBySlot(equipmentSlot);

        return CanEquipeItem(itemInEquipment, bodyPartSlot);
    }

    public void EquipeItemInEquipmentOnThisBodyPart(int equipmentSlot, BodyPartSlot bodyPartSlot)
    {
        Item itemInEquipment = GetItemBySlot(equipmentSlot);
        Item equipedItem = GetEquipedItem(bodyPartSlot);

        EquipeItem(itemInEquipment, bodyPartSlot);
        SetSlot(equipedItem, equipmentSlot);
    }

    public bool TryEquipeItemInEquipmentOnThisBodyPart(int equipmentSlot, BodyPartSlot bodyPartSlot)
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

    public bool CanChangeEquipedItems(BodyPartSlot slot1, BodyPartSlot slot2)
    {
        Item item1 = GetEquipedItem(slot1);
        Item item2 = GetEquipedItem(slot2);

        return CanEquipeItem(item1, slot2) && CanEquipeItem(item2, slot1);
    }

    public void ChangeEquipedItems(BodyPartSlot slot1, BodyPartSlot slot2)
    {
        Item item1 = GetEquipedItem(slot1);
        Item item2 = GetEquipedItem(slot2);

        EquipeItem(item1, slot2);
        EquipeItem(item2, slot1);
    }

    public bool TryChangeEquipedItems(BodyPartSlot slot1, BodyPartSlot slot2)
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
}
