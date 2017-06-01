using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.BodyParts;
using UnityEngine;

namespace NetworkProject.Items
{
    public class PlayerEquipedItems
    {
        private List<BodyPart> _bodyParts;

        public PlayerEquipedItems(PlayerEquipedItemsPackage package)
        {
            _bodyParts = new List<BodyPart>(PackageConverter.PackageToBodyPart(package.BodyParts.ToArray()));
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

        public bool CanEquipeItem(Item item, PlayerStats stats)
        {
            EquipableItemData itemData = (EquipableItemData)ItemRepository.GetItemByIdItem(item.IdItem);

            return itemData.CanEquip(stats);
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

        public bool IsEquipedWeapon()
        {
            foreach (var bodyPart in _bodyParts)
            {
                if (bodyPart.EquipedItem == null)
                {
                    continue;
                }

                EquipableItemData item = (EquipableItemData)ItemRepository.GetItemByIdItem(bodyPart.EquipedItem.IdItem);

                if (item is WeaponData)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

