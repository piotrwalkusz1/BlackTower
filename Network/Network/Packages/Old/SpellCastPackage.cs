using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject
{
    public class SpellCastPackage : INetworkPackage
    {
        public int _idSpell;

        public virtual void Set(IncomingMessage message)
        {
            _idSpell = message.ReadInt();
        }

        public virtual byte[] ToBytes()
        {
            var data = new OutgoingMessage();

            data.Write(_idSpell);

            return data.Data.ToArray();
        }
    }
}
