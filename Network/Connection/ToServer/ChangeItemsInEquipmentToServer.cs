﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToServer
{
    [Serializable]
    public class ChangeItemsInEquipmentToServer : INetworkRequestToServer
    {
        public int Slot1 { get; set; }
        public int Slot2 { get; set; }

        public ChangeItemsInEquipmentToServer(int slot1, int slot2)
        {
            Slot1 = slot1;
            Slot2 = slot2;
        }
    }
}
