using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Items;

namespace NetworkProject.Connection.ToServer
{
    [Serializable]
    public class BuyItemInShopToServer : INetworkRequestToServer
    {
        public int IdNetNPC { get; set; }
        public int Slot { get; set; }

        public BuyItemInShopToServer(int idNetNPC, int slot)
        {
            IdNetNPC = idNetNPC;
            Slot = slot;
        }
    }
}
