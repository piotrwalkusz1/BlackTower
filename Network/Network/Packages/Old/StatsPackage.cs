using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject
{
    public abstract class StatsPackage : INetworkPackage
    {
        public abstract void Set(IncomingMessage message);

        public abstract byte[] ToBytes();
    }
}
