using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject
{
    public class IntPackage : INetworkPackage
    {
        public int Int { get; set; }

        public IntPackage()
        {

        }

        public IntPackage(int i)
        {
            Int = i;
        }

        public void Set(IncomingMessage message)
        {
            Int = message.ReadInt();
        }

        public byte[] ToBytes()
        {
            var data = new OutgoingMessage();

            data.Write(Int);

            return data.Data.ToArray();
        }

        public static implicit operator int(IntPackage intPackage)
        {
            return intPackage.Int;
        }

        public static implicit operator IntPackage(int i)
        {
            return new IntPackage(i);
        }
    }
}
