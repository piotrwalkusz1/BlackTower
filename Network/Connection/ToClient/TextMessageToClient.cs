using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Connection;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class TextMessageToClient : INetworkRequestToClient
    {
        public int IdMessage { get; set; }

        public TextMessageToClient(int idMessage)
        {
            IdMessage = idMessage;
        }
    }
}
