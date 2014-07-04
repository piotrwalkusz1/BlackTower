﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace NetworkProject.Items.Repository
{
    [Serializable]
    public class Item
    {
        public virtual int IdItem
        {
            get
            {
                return _idItem;
            }
            set
            {
                _idItem = value;
            }
        }

        private int _idItem;

        public Item()
        {

        }

        public Item(int idItem)
        {
            _idItem = idItem;
        }

        public virtual void SetStats(XmlNodeList stats)
        {
            _idItem = int.Parse(stats[0].InnerText);
        }
    }
}
