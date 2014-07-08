using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Items
{
    [Serializable]
    public class VisualEquipableItemData : VisualItemData
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
                return _itemData;
            }
        }
        public int IdPrefabOnPlayer { get; set; }

        private EquipableItemData _itemData;

        public VisualEquipableItemData(EquipableItemData itemData)
        {
            _itemData = itemData;
        }
    }
}
