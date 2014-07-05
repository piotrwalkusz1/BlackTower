using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class UpdateMaxHP : INetworkRequest
    {
        public int IdNet { get; set; }
        public int MaxHP { get; set; }

        public UpdateMaxHP(int idNet, int maxHp)
        {
            IdNet = idNet;
            maxHp = MaxHP;
        }
    }
}
