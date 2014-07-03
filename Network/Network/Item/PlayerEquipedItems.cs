using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.BodyParts;

namespace NetworkProject.Items
{
    public class PlayerEquipedItems : EquipedItems
    {
        private List<BodyPart> _bodyParts;

        public PlayerEquipedItems()
        {
            _bodyParts.Add(new BodyParts.Head());
            _bodyParts.Add(new BodyParts.Chest());
            _bodyParts.Add(new BodyParts.Feet());
            _bodyParts.Add(new BodyParts.RightHand());
            _bodyParts.Add(new BodyParts.LeftHand());
            _bodyParts.Add(new BodyParts.Other());
            _bodyParts.Add(new BodyParts.Other());
        }

        public Item GetEquipedItem(BodyPartType slot)
        {
            return _bodyParts[(int)slot].EquipedItem;
        }

        public bool CanEquipeItemOnThisBodyPart(ItemInfo item, BodyPartType bodyPart)
        {
            return _bodyParts[(int)bodyPart].CanEquipeItemOnThisBodyPart(item);
        }

        public bool IsEmptySlot(BodyPartType bodyPart)
        {
            return GetEquipedItem(bodyPart) == null;
        }

        public void EquipeItem(Item item, BodyPartType bodyPartType)
        {
            BodyPart bodyPart = _bodyParts[(int)bodyPartType];

            bodyPart.EquipedItem = item;
        }
    }
}

