using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject.Items;
using NetworkProject.Connection;
using NetworkProject.Connection.ToClient;

public class Merchant : MonoBehaviour
{
    public List<int> _itemsId;
    public List<int> _itemsPrice;

    private List<ItemInShop> _items;

    private const int MAX_ITEMS = 36;

    void Awake()
    {
        _items = new List<ItemInShop>();

        for (int i = 0; i < _itemsId.Count; i++)
        {
            var itemInShop = new ItemInShop(new Item(_itemsId[i]), _itemsPrice[i]);

            _items.Add(itemInShop);
        }
    }

    public void AddItems(ItemInShop[] items)
    {
        if (items.Length + _items.Count > MAX_ITEMS)
        {
            throw new InvalidOperationException("Kupiec nie może mieć więcej itemów do sprzedawania");
        }

        _items.AddRange(items);
    }

    public void OpenShopInClient(IConnectionMember address)
    {
        var shop = new ShopPackage(PackageConverter.ItemInShopToPackage(_items.ToArray()).ToArray());

        var request = new OpenShopToClient(shop, GetComponent<NetNPC>().IdNet);

        Server.SendRequestAsMessage(request, address);
    }

    public void SellItem(int slot, NetPlayer player)
    {
        ItemInShop item = _items[slot];

        PlayerEquipment eq = player.GetComponent<PlayerEquipment>();

        int emptySlot = eq.GetEmptyBagSlot();

        if (emptySlot == -1)
        {
            throw new InvalidOperationException("Gracz nie ma pustego miejsca w ekwipunku");
        }

        Vector3 offset = player.transform.position - transform.position;

        if (offset.sqrMagnitude > NetworkProject.Settings.talkNPCRange * NetworkProject.Settings.talkNPCRange)
        {
            throw new InvalidOperationException("Gracz jest za daleko");
        }

        if (eq.Gold < item.Price)
        {
            throw new InvalidOperationException("Gracz nie ma dość złota");
        }

        eq.Gold -= item.Price;
        eq.SetItemInBag(item.Item, emptySlot);

        eq.SendUpdateGold(player.OwnerAddress);
        eq.SendUpdateBagSlot(emptySlot, player.OwnerAddress);
    }
}
