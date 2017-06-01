using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Connection.ToServer;

public class Shop
{
    public List<ItemInShop> Items { get; set; }

    public Shop(ItemInShop[] items)
    {
        Items = new List<ItemInShop>(items);
    }

    public void BuyItem(int idNetNPC, OwnPlayerEquipment buyer, int slot)
    {
        if (!buyer.IsEmptyAnyBagSlot())
        {
            throw new InvalidOperationException("Kupujący nie ma miejsca w ekwipunku.");
        }

        ItemInShop item;

        try
        {
            item = Items[slot];
        }
        catch (IndexOutOfRangeException)
        {
            throw new ArgumentOutOfRangeException("Numer slotu jest poza zasięgiem");
        }

        if (item == null)
        {
            throw new InvalidOperationException("Nie ma żadnego itemu w podanym slocie");
        }

        if (item.Price > buyer.Gold)
        {
            throw new InvalidOperationException("Kupujący nie ma wystarczająco pieniędzy");
        }

        var request = new BuyItemInShopToServer(idNetNPC, slot);

        Client.SendRequestAsMessage(request);
    }
}

