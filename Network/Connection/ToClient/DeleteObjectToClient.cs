using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class DeleteObjectToClient : INetworkRequestToClient
    {
        public int IdNet { get; set; }

        public DeleteObjectToClient(int idNet)
        {
            IdNet = idNet;
        }
    }
}
