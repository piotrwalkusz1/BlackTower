using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace NetworkProject.Connection
{
    public class IncomingMessageFromServer
    {
        public INetworkRequest Request { get; set; }

        public IncomingMessageFromServer(byte[] request)
        {
            var formatter = new BinaryFormatter();
            var stream = new MemoryStream(request);

            Request = (INetworkRequest)formatter.Deserialize(stream);
        }

        public IncomingMessageFromServer(INetworkRequest request)
        {
            Request = request;
        }

        public IncomingMessageFromServer(IncomingMessageFromServer message)
        {
            Request = message.Request;
        }
    }
}
