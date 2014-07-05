using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class Jump : INetworkRequest
    {
        public int IdNet { get; set; }

        public Jump(int idNet)
        {
            IdNet = idNet;
        }
    }
}
