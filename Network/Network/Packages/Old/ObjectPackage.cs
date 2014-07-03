using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject
{
    public class ObjectPackage : INetworkPackage
    {
        public int IdObject { get; set; }

        public virtual void Set(IncomingMessage message)
        {
            IdObject = message.ReadInt();
        }

        public virtual byte[] ToBytes()
        {
            OutgoingMessage message = new OutgoingMessage();
            message.Write(IdObject);
            return message.Data.ToArray();
        }
    }
}
