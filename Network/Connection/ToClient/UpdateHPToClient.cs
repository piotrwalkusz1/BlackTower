using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class UpdateHPToClient : INetworkRequest
    {
        public int NetId { get; set; }
        public int HP { get; set; }

        public UpdateHPToClient(int netId, int hp)
        {
            NetId = netId;
            HP = hp;
        }
    }
}
