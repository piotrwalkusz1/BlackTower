using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject
{
    public class WorldInfoPackage : INetworkPackage
    {
        public int MapNumber { get; set; }

        public virtual void Set(IncomingMessage message)
        {
            MapNumber = message.ReadInt();
        }

        public virtual byte[] ToBytes()
        {
            OutgoingMessage message = new OutgoingMessage();
            message.Write(MapNumber);

            return message.Data.ToArray();
        }
    }
}
