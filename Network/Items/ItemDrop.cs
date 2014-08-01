using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Items
{
    [Serializable]
    public struct ItemDrop
    {
        public Item Item
        {
            get
            {
                return _item;
            }
        }
        public float Chances
        {
            get
            {
                return _chances;
            }
        }

        public Item _item;
        public float _chances;

        public ItemDrop(Item item, float chances)
        {
            _item = item;
            _chances = chances;
        }
    }
}
