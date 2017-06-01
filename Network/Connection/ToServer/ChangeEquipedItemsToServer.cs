using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.BodyParts;

namespace NetworkProject.Connection.ToServer
{
    [Serializable]
    public class ChangeEquipedItemsToServer : INetworkRequestToServer
    {
        public int Slot1 { get; set; }
        public int Slot2 { get; set; }

        public ChangeEquipedItemsToServer(int slot1, int slot2)
        {
            Slot1 = slot1;
            Slot2 = slot2;
        }
    }
}
