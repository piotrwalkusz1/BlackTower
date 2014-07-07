﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Items
{
    [Serializable]
    public class ArmorData : EquipableItemData
    {
        public int _defense;

        public ArmorData()
        {

        }

        public ArmorData(int idItem)
        {
            IdItem = idItem;
        }

        protected override void ApplyItemStats(IEquipableStats stats)
        {
            stats.Defense += _defense;
        }
    }
}
