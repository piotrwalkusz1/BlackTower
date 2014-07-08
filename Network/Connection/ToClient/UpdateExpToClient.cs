using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class UpdateExpToClient : INetworkRequest
    {
        public int IdNet { get; set; }
        public int Exp { get; set; }

        public UpdateExpToClient(int idNet, int exp)
        {
            IdNet = idNet;
            Exp = exp;
        }
    }
}
