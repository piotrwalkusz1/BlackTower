using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Items.Repository
{
    [Serializable]
    public class VisualEquipableItem : VisualItem
    {
        public override Item ItemData
        {
            get
            {
                return _itemData;
            }
        }
        public EquipableItem EquipableItemData
        {
            get
            {
                return _itemData;
            }
        }
        public int IdPrefabOnPlayer { get; set; }

        private EquipableItem _itemData;

        public VisualEquipableItem(EquipableItem itemData)
        {
            _itemData = itemData;
        }
    }
}
