using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class UpdateMaxHPToClient : INetworkRequestToClient
    {
        public int IdNet { get; set; }
        public int MaxHP { get; set; }

        public UpdateMaxHPToClient(int idNet, int maxHp)
        {
            IdNet = idNet;
            maxHp = MaxHP;
        }
    }
}
