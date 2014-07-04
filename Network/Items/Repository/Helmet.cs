using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Items.Repository
{
    public class Helmet : EquipableItem
    {
        public int _defense;

        public Helmet()
        {

        }

        public Helmet(int idItem)
        {
            IdItem = idItem;
        }

        public override void ApplyItemStats(Stats stats)
        {
            stats.Defense += _defense;
        }
    }
}
