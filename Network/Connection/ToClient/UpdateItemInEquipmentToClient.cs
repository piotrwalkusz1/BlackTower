using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Items;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class UpdateItemInEquipmentToClient : INetworkRequest
    {
        public int IdNet { get; set; }
        public int Slot { get; set; }
        public Item Item { get; set; }

        public UpdateItemInEquipmentToClient(int idNet, int slot, Item item)
        {
            IdNet = idNet;
            Slot = slot;
            Item = item;
        }
    }
}
