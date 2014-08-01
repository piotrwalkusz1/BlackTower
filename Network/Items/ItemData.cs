using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Items
{
    [Serializable]
    public class ItemData
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

        protected int _idItem;

        //Xml serialization require this constructor
        public ItemData()
        {

        }

        public ItemData(int idItem)
        {
            _idItem = idItem;
        }
    }
}
