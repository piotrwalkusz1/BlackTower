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
        public float _attackSpeed;

        public WeaponData()
        {

        }

        public WeaponData(int idItem)
        {
            IdItem = idItem;
        }

        public override void ApplyItemStats(IStats stats)
        {
            stats.AttackSpeed += stats.AttackSpeed;
        }
    }
}
