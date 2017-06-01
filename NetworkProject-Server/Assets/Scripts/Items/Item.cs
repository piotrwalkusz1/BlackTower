using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;

public class Item
{
    public int IdItem { get; set; }
    public virtual ItemData ItemData
    {
        get { return ItemRepository.GetItemByIdItem(IdItem); }
    }

    public Item(int idItem)
    {
        IdItem = idItem;
    }

    public bool CanEquip(PlayerStats stats)
    {
        if (ItemData is EquipableItemData)
        {
            return ((EquipableItemData)ItemData).CanEquipe(stats);
        }
        else
        {
            return false;
        }
    }
}
