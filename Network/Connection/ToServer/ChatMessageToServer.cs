using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToServer
{
    [Serializable]
    public class ChatMessageToServer : INetworkRequestToServer
    {
        public string Message { get; set; }

        public ChatMessageToServer(string message)
        {
            Message = message;
        }
    }
}
