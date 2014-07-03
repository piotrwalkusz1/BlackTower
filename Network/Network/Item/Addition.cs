using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace NetworkProject.Items
{
    public class Addition : EquipableItem
    {
        public Addition()
        {

        }

        public Addition(int idItem)
        {
            _idItem = idItem;
        }

        public override void SetStats(XmlNodeList data)
        {
            _idItem = int.Parse(data[0].InnerText);
        }   
       
        public override void ApplyItemStats(Stats stats)
        {
 	        
        }
    }
}
