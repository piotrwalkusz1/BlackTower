using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ItemReward : IReward
{
    public Item Item { get; set; }

    public ItemReward(Item item)
    {
        Item = item;
    }

    public int GetIdImage()
    {
        return Item.ItemData.IdTexture;
    }
}
