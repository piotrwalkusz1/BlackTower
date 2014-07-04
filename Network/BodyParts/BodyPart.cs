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
            var itemData = Items.Repository.ItemRepository.GetItemByIdItem(item.IdItem);

            return CanEquipeItemOnThisBodyPart(itemData);
        }

        public abstract bool CanEquipeItemOnThisBodyPart(Items.Repository.Item item);
    }
}


