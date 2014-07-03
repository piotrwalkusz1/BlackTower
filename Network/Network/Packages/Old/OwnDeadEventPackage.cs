using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject
{
    public class OwnDeadEventPackage : INetworkPackage
    {
        public virtual void Set(IncomingMessage message)
        {

        }

        public virtual byte[] ToBytes()
        {
            OutgoingMessage data = new OutgoingMessage();

            return data.Data.ToArray();
        }
    }
}
