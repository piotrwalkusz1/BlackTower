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
            _bodyParts = new List<BodyPart>(IoC.GetBodyParts());
        }

        public Item GetEquipedItem(int slot)
        {
            return _bodyParts[slot].EquipedItem;
        }

        public BodyPart[] GetBodyParts()
        {
            return _bodyParts.ToArray();
        }

        public bool CanEquipeItem(Item item, IEquipableStats stats)
        {
            EquipableItemData itemData = (EquipableItemData)ItemRepository.GetItemByIdItem(item.IdItem);

            return itemData.CanEquipe(stats);
        }

        public bool IsEmptySlot(int bodyPart)
        {
            return GetEquipedItem(bodyPart) == null;
        }

        public void EquipeItem(Item item, int bodyPartType)
        {
            BodyPart bodyPart = _bodyParts[(int)bodyPartType];

            bodyPart.EquipedItem = item;
        }

        public override void ApplyToStats(IEquipableStats player)
        {
            foreach (var bodyPart in _bodyParts)
            {
                bodyPart.ApplyEquipedItemToStats(player);
            }
        }

        public bool IsEquipedWeapon()
        {
            foreach (var bodyPart in _bodyParts)
            {
                ItemData item = ItemRepository.GetItemByIdItem(bodyPart.EquipedItem.IdItem);

                if (item is WeaponData)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

