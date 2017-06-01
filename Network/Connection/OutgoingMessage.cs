using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

namespace NetworkProject.Connection
{
    public class OutgoingMessage
    {
        public INetworkRequest Request { get; set; }

        public OutgoingMessage(INetworkRequest request)
        {
            Request = request;
        }

        public byte[] GetBytes()
        {
            var formatter = new BinaryFormatter();
            var stream = new MemoryStream();

            formatter.Serialize(stream, Request);

            return stream.GetBuffer();
        }
    }
}
