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

    public bool CanEquipeItemOnThisBodyPart(Item item, BodyPartSlot bodyPart)
    {
        return _equipedItems.CanEquipeItemOnThisBodyPart(item, bodyPart);
    }
}
