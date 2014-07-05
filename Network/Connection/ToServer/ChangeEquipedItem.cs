using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.BodyParts;

namespace NetworkProject.Connection.ToServer
{
    [Serializable]
    public class ChangeEquipedItem : INetworkRequest
    {
        public int SlotInEquipment { get; set; }
        public BodyPartSlot EquipedItem { get; set; }

        public ChangeEquipedItem(int slotInEquipment, BodyPartSlot equipedItem)
        {
            SlotInEquipment = slotInEquipment;
            EquipedItem = equipedItem;
        }
    }
}
