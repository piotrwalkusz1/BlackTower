using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Items
{
    [Serializable]
    public struct ItemDrop
    {
        public ItemPackage Item
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

        public ItemPackage _item;
        public float _chances;

        public ItemDrop(ItemPackage item, float chances)
        {
            _item = item;
            _chances = chances;
        }
    }
}
