using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ItemInShop
{
    public Item Item { get; set; }
    public int Price { get; set; }

    public ItemInShop(Item item, int price)
    {
        Item = item;
        Price = price;
    }
}
