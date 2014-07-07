using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;
using NetworkProject.Items;

namespace NetworkProject.BodyParts
{
    public abstract class BodyPart
    {
        public Item EquipedItem { get; set; }

        public bool CanEquipeItemOnThisBodyPart(Item item)
        {
            var itemData = ItemRepository.GetItemByIdItem(item.IdItem);

            return CanEquipeItemOnThisBodyPart(itemData);
        }

        public abstract bool CanEquipeItemOnThisBodyPart(ItemData item);

        public void ApplyEquipedItemToStats(IEquipableStats stats)
        {
            EquipableItemData item = (EquipableItemData)ItemRepository.GetItemByIdItem(EquipedItem.IdItem);

            item.ApplyToStats(stats);
        }
    }
}


