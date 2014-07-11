using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace NetworkProject.Items
{
    [Serializable]
    public class AdditionData : EquipableItemData
    {
        public AdditionData()
        {

        }

        public AdditionData(int idItem)
        {
            IdItem = idItem;
        }
       
        protected override void ApplyItemStats(IEquipableStats stats)
        {
 	        
        }
    }
}
