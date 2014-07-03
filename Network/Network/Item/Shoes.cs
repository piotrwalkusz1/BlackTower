using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Items
{
    public class Shoes : EquipableItem
    {
        public float _movementSpeed;

        public Shoes()
        {

        }

        public Shoes(int idItem)
        {
            _idItem = idItem;
        }

        public override void ApplyItemStats(Stats stats)
        {
            stats.MovementSpeed += _movementSpeed;
        }
    }
}
