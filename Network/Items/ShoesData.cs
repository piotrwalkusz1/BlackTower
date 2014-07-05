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

        public ShoesData()
        {

        }

        public ShoesData(int idItem)
        {
            IdItem = idItem;
        }

        public override void ApplyItemStats(IStats stats)
        {
            stats.MovementSpeed += _movementSpeed;
        }
    }
}
