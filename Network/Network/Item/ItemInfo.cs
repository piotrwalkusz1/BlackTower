using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace NetworkProject.Items
{
    public class ItemInfo
    {
        public int _idItem;

        public ItemInfo()
        {

        }

        public ItemInfo(int idItem)
        {
            _idItem = idItem;
        }

        public virtual void SetStats(XmlNodeList stats)
        {
            _idItem = int.Parse(stats[0].InnerText);
        }
    }
}
