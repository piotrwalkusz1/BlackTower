using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject
{
    public class NamedObjectPackage : ObjectPackage
    {
        public string Name { get; set; }

        public override void Set(IncomingMessage message)
        {
            base.Set(message);

            Name = message.ReadString();
        }

        public override byte[] ToBytes()
        {
            byte[] bytes = base.ToBytes();

            OutgoingMessage message = new OutgoingMessage();

            message.Write(bytes);

            message.Write(Name);

            return message.Data.ToArray();
        }
    }
}
