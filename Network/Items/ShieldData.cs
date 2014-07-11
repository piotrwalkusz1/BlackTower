using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Items
{
    [Serializable]
    public class ShieldData : EquipableItemData
    {
        public int _defense;

        public ShieldData()
        {

        }

        public ShieldData(int idItem) : base(idItem)
        {

        }

        protected override void ApplyItemStats(IEquipableStats stats)
        {
            stats.Defense += _defense;
        }
    }
}
