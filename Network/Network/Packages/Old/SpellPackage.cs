using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject
{
    public class SpellPackage : INetworkPackage
    {
        public int _idSpell;
        public DateTime _nextUseTime;

        public SpellPackage()
        {

        }

        public SpellPackage(int idSpell, DateTime nextUseTime)
        {
            _idSpell = idSpell;
            _nextUseTime = nextUseTime;
        }

        public void Set(IncomingMessage message)
        {
            _idSpell = message.ReadInt();
            _nextUseTime = message.ReadDateTime();
        }

        public byte[] ToBytes()
        {
            var data = new OutgoingMessage();

            data.Write(_idSpell);
            data.Write(_nextUseTime);

            return data.Data.ToArray();
        }
    }
}
