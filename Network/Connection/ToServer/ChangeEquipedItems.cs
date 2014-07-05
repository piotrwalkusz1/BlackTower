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
        public BodyPartSlot EquipedItem1;
        public BodyPartSlot EquipedItem2;

        public ChangeEquipedItems(BodyPartSlot equipedItem1, BodyPartSlot equipedItem2)
        {
            EquipedItem1 = equipedItem1;
            EquipedItem2 = equipedItem2;
        }
    }
}
