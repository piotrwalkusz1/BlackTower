using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Items;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class UpdateItemInEquipmentToClient : INetworkRequestToClient
    {
        public int IdNet { get; set; }
        public int Slot { get; set; }
        public ItemPackage Item { get; set; }

        public UpdateItemInEquipmentToClient(int idNet, int slot, ItemPackage item)
        {
            IdNet = idNet;
            Slot = slot;
            Item = item;
        }
    }
}
