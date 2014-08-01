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

        public WeaponData(int idItem)
            : base(idItem)
        {

        }

        protected override void ApplyItemStats(IEquipableStats stats)
        {
            stats.MinDmg += _minDmg;
            stats.MaxDmg += _maxDmg;
            stats.AttackSpeed += _attackSpeed;
        }
    }
}
