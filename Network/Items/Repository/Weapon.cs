using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Items.Repository
{
    [Serializable]
    public class Weapon : EquipableItem
    {
        public int _minDmg;
        public int _maxDmg;
        public float _attackSpeed;

        public Weapon()
        {

        }

        public Weapon(int idItem)
        {
            IdItem = idItem;
        }

        public override void ApplyItemStats(IStats stats)
        {
            stats.AttackSpeed += stats.AttackSpeed;
        }
    }
}
