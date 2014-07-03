using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Items
{
    public class Shield : EquipableItem
    {
        public int _defense;

        public Shield()
        {

        }

        public Shield(int idItem)
        {
            _idItem = idItem;
        }

        public override void ApplyItemStats(Stats stats)
        {
            stats.Defense += _defense;
        }
    }
}
