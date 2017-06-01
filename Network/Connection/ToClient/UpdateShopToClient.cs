using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Items;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class UpdateShopToClient : INetworkRequestToClient
    {
        public ItemInShopPackage Item { get; set; }
        public int Slot { get; set; }

        public UpdateShopToClient(ItemInShopPackage item, int slot)
        {
            Item = item;
            Slot = slot;
        }
    }
}
