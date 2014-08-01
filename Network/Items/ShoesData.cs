using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Items
{
    [Serializable]
    public class ShoesData : EquipableItemData
    {
        public float _movementSpeed;

        public ShoesData(int idItem)
            : base(idItem)
        {

        }

        protected override void ApplyItemStats(IEquipableStats stats)
        {
            stats.MovementSpeed += _movementSpeed;
        }
    }
}
