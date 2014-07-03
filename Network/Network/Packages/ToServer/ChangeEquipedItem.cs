using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.BodyParts;

namespace NetworkProject.Packages.ToServer
{
    [Serializable]
    public class ChangeEquipedItem : INetworkPackage
    {
        public int SlotInEquipment { get; set; }
        public BodyPartType EquipedItem { get; set; }

        public ChangeEquipedItem(int slotInEquipment, BodyPartType equipedItem)
        {
            SlotInEquipment = slotInEquipment;
            EquipedItem = equipedItem;
        }
    }
}
