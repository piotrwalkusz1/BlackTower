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

        public ShieldData(int idItem) : base(idItem)
        {

        }

        public override void ApplyItemStats(IStats stats)
        {
            stats.Defense += _defense;
        }
    }
}
