using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

namespace NetworkProject.Connection
{
    public class IncomingMessage
    {
        public IConnectionMember Sender { get; set; }
        public INetworkRequest Request { get; set; }

        public IncomingMessage(byte[] request, IConnectionMember sender)
        {
            Sender = sender;

            var formatter = new BinaryFormatter();
            var stream = new MemoryStream(request);

            Request = (INetworkRequest)formatter.Deserialize(stream);
        }

        public IncomingMessage(INetworkRequest request, IConnectionMember sender)
        {
            Request = request;
            Sender = sender;
        }
    }
}
