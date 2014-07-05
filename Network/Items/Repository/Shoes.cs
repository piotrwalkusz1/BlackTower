using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Items.Repository
{
    [Serializable]
    public class Shoes : EquipableItem
    {
        public float _movementSpeed;

        public Shoes()
        {

        }

        public Shoes(int idItem)
        {
            IdItem = idItem;
        }

        public override void ApplyItemStats(IStats stats)
        {
            stats.MovementSpeed += _movementSpeed;
        }
    }
}
