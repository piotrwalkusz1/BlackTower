﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace NetworkProject.Items
{
    [Serializable]
    public class AdditionData : EquipableItemData
    {
        public AdditionData(int idItem)
            : base(idItem)
        {

        }
       
        protected override void ApplyItemStats(IEquipableStats stats)
        {
 	        
        }
    }
}
