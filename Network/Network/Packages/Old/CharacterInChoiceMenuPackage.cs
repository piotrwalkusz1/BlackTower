using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject
{
    public class CharacterInChoiceMenuPackage : INetworkPackage
    {
        public string Name { get; set; }

        public virtual void Set(IncomingMessage message)
        {
            Name = message.ReadString();
        }

        public virtual byte[] ToBytes()
        {
            OutgoingMessage data = new OutgoingMessage();
            data.Write(Name);
            return data.Data.ToArray();
        }
    }
}
