using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Items
{
    [Serializable]
    public class ArmorData : EquipableItemData
    {
        public int _defense;

        public ArmorData()
        {

        }

        public ArmorData(int idItem)
        {
            IdItem = idItem;
        }

        public override void ApplyItemStats(IStats stats)
        {
            stats.Defense += _defense;
        }
    }
}
