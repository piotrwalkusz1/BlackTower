using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Items
{
    [Serializable]
    public class WeaponData : EquipableItemData
    {
        public int _minDmg;
        public int _maxDmg;
        public int _attackSpeed;

        public WeaponData()
        {

        }

        public WeaponData(int idItem)
        {
            IdItem = idItem;
        }

        protected override void ApplyItemStats(IEquipableStats stats)
        {
            IEquipableStats equipableStats = (IEquipableStats)stats;

            equipableStats.AttackSpeed += equipableStats.AttackSpeed;
        }
    }
}
