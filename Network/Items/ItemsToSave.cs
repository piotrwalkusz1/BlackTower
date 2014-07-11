using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Items
{
    public class ItemsToSave
    {
        public ItemData[] Items { get; private set; }

        //xml serializer require 0-argument constructor
        public ItemsToSave()
        {

        }

        public ItemsToSave(ItemData[] items)
        {
            Items = items;
        }
    }
}
