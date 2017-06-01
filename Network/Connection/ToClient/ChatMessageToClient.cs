using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class ChatMessageToClient : INetworkRequestToClient
    {
        public string Message { get; set; }

        public ChatMessageToClient(string message)
        {
            Message = message;
        }
    }
}
