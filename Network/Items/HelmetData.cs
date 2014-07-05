using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Items
{
    public class HelmetData : EquipableItemData
    {
        public int _defense;

        public HelmetData()
        {

        }

        public HelmetData(int idItem)
        {
            IdItem = idItem;
        }

        public override void ApplyItemStats(IStats stats)
        {
            stats.Defense += _defense;
        }
    }
}
