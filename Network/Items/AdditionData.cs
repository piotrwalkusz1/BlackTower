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
        public AdditionData(int idItem)
        {
            IdItem = idItem;
        }

        public override void SetStats(XmlNodeList data)
        {
            IdItem = int.Parse(data[0].InnerText);
        }   
       
        public override void ApplyItemStats(IStats stats)
        {
 	        
        }
    }
}
