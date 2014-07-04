using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Items.Repository
{
    [Serializable]
    public class Shield : EquipableItem
    {
        public int _defense;

        public Shield(int idItem) : base(idItem)
        {

        }

        public override void ApplyItemStats(Stats stats)
        {
            stats.Defense += _defense;
        }
    }
}
