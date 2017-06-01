using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToServer
{
    [Serializable]
    public class UseTalismanToServer : INetworkRequestToServer
    {
        public int Slot { get; set; }

        public UseTalismanToServer(int slot)
        {
            Slot = slot;
        }
    }
}
