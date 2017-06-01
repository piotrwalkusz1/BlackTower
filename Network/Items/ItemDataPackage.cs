using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Items
{
    [Serializable]
    public class ItemDataPackage
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

        public ItemDataPackage(int idItem)
        {
            _idItem = idItem;
        }
    }
}
