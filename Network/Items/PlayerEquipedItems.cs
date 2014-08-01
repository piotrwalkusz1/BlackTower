using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.BodyParts;
using UnityEngine;

namespace NetworkProject.Items
{
    [Serializable]
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

        public BodyPart GetBodyPart(int bodyPartSlot)
        {
            return _bodyParts[bodyPartSlot];
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
                if (bodyPart.EquipedItem == null)
                {
                    continue;
                }

                IEquipableItemManager item = (IEquipableItemManager)ItemRepository.GetItemByIdItem(bodyPart.EquipedItem.IdItem);
                EquipableItemData equipableItem = item.GetEquipableItemData();

                if (equipableItem is WeaponData)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

