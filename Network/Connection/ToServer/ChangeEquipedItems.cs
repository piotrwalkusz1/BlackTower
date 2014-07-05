using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.BodyParts;

namespace NetworkProject.Connection.ToServer
{
    [Serializable]
    public class ChangeEquipedItems : INetworkRequest
    {
        public BodyPartSlot Slot1;
        public BodyPartSlot Slot2;

        public ChangeEquipedItems(BodyPartSlot slot1, BodyPartSlot slot2)
        {
            Slot1 = slot1;
            Slot2 = slot2;
        }
    }
}
