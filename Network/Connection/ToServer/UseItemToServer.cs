using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToServer
{
    [Serializable]
    public class UseItemToServer : INetworkRequestToServer
    {
        public int Slot { get; set; }

        public UseItemToServer(int slot)
        {
            Slot = slot;
        }
    }
}
