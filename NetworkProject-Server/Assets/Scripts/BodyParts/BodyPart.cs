using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;
using NetworkProject.Items;

public abstract class BodyPart
{
    public Item EquipedItem { get; set; }
    public EquipableItemData EquipedItemData
    {
        get { return (EquipableItemData)ItemRepository.GetItemByIdItem(EquipedItem.IdItem); }
    }

    public BodyPart(Item item)
    {
        EquipedItem = item;
    }

    public bool CanEquipeItemOnThisBodyPart(Item item)
    {
        var itemData = ItemRepository.GetItemByIdItem(item.IdItem);

        if (itemData is EquipableItemData)
        {
            return CanEquipeItemOnThisBodyPart((EquipableItemData)itemData);
        }
        else
        {
            return false;
        }          
    }

    public abstract bool CanEquipeItemOnThisBodyPart(EquipableItemData item);

    public void ApplyEquipedItemStats(PlayerStats stats)
    {
        if (EquipedItem != null)
        {
            EquipedItemData.ApplyItemStats(stats);
        }
    }

    public void ApplyEquipedItemBenefits(PlayerStats stats)
    {
        if (EquipedItem != null)
        {
            EquipedItemData.ApplyItemBenefits(stats);
        }
    }
}


