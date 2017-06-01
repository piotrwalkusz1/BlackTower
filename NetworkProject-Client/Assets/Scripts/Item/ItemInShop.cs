using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Standard;

public class ItemInShop
{
    public Item Item { get; set; }
    public int Price { get; set; }

    public ItemInShop(Item item, int price)
    {
        Item = item;
        Price = price;
    }

    public string GetDescription()
    {
        string result = Languages.GetPhrase("price") + " : ";
        result += Price.ToString();
        result += "\n\n";
        result += Item.GetDescription();

        return result;
    }
}
