using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Items
{
    public class Helmet : EquipableItem
    {
        public int _defense;

        public Helmet()
        {

        }

        public Helmet(int idItem)
        {
            _idItem = idItem;
        }

        public override void ApplyItemStats(Stats stats)
        {
            stats.Defense += _defense;
        }
    }
}
