using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToServer
{
    [Serializable]
    public class PickItemToServer : INetworkRequest
    {
        public int IdNetItem { get; set; }

        public PickItemToServer(int idNetItem)
        {
            IdNetItem = idNetItem;
        }
    }
}
