using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class UpdateManaToClient : INetworkRequestToClient
    {
        public int NetId { get; set; }
        public int Mana { get; set; }

        public UpdateManaToClient(int netId, int mana)
        {
            NetId = netId;
            Mana = mana;
        }
    }
}
