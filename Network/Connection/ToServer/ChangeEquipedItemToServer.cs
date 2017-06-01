using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.BodyParts;

namespace NetworkProject.Connection.ToServer
{
    [Serializable]
    public class ChangeEquipedItemToServer : INetworkRequestToServer
    {
        public int SlotInEquipment { get; set; }
        public int EquipedItemSlot { get; set; }

        public ChangeEquipedItemToServer(int slotInEquipment, int equipedItem)
        {
            SlotInEquipment = slotInEquipment;
            EquipedItemSlot = equipedItem;
        }
    }
}
