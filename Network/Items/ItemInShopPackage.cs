using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Items
{
    [Serializable]
    public class ItemInShopPackage
    {
        public ItemPackage Item { get; set; }
        public int Price { get; set; }

        public ItemInShopPackage(ItemPackage item, int price)
        {
            Item = item;
            Price = price;
        }
    }
}
