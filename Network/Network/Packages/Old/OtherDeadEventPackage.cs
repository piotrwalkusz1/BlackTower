using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject
{
    public class OtherDeadEventPackage : INetworkPackage
    {
        public int _idPlayer;

        public virtual void Set(IncomingMessage message)
        {
            _idPlayer = message.ReadInt();
        }

        public virtual byte[] ToBytes()
        {
            OutgoingMessage data = new OutgoingMessage();

            data.Write(_idPlayer);

            return data.Data.ToArray();
        }
    }
}
