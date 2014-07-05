using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Items.Repository
{
    [Serializable]
    public class Armor : EquipableItem
    {
        public int _defense;

        public Armor()
        {

        }

        public Armor(int idItem)
        {
            IdItem = idItem;
        }

        public override void ApplyItemStats(IStats stats)
        {
            stats.Defense += _defense;
        }
    }
}
