using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Benefits;
using NetworkProject.Requirements;

namespace NetworkProject.Items
{
    [Serializable]
    public class VisualEquipableItemData : VisualItemData, IEquipableItemManager
    {
        public override ItemData ItemData
        {
            get
            {
                return _itemData;
            }
        }
        public EquipableItemData EquipableItemData
        {
            get
            {
                return (EquipableItemData)_itemData;
            }
        }
        public int IdPrefabOnPlayer { get; set; }

        public VisualEquipableItemData(EquipableItemData itemData)
            : base(itemData)
        {

        }

        public static implicit operator EquipableItemData(VisualEquipableItemData item)
        {
            return item.EquipableItemData;
        }

        public EquipableItemData GetEquipableItemData()
        {
            return EquipableItemData;
        }
    }
}
