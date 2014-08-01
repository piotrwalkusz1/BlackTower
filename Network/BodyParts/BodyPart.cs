using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;
using NetworkProject.Items;

namespace NetworkProject.BodyParts
{
    [Serializable]
    public abstract class BodyPart
    {
        public Item EquipedItem { get; set; }

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

        public void ApplyEquipedItemToStats(IEquipableStats stats)
        {
            if (EquipedItem != null)
            {
                EquipableItemData item = (EquipableItemData)ItemRepository.GetItemByIdItem(EquipedItem.IdItem);

                item.ApplyToStats(stats);
            }
        }
    }
}


