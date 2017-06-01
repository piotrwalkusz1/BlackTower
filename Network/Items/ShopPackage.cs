using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NetworkProject.Items
{
    [Serializable]
    public class ShopPackage
    {
        public List<ItemInShopPackage> Items { get; set; }

        public ShopPackage(ItemInShopPackage[] items)
        {
            Items = new List<ItemInShopPackage>(items);
        }
    }
}
