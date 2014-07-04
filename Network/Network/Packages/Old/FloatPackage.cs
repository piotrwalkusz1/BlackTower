using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject
{
    public class FloatPackage : INetworkPackage
    {
        public float Float { get; set; }

        public FloatPackage()
        {

        }

        public FloatPackage(float f)
        {
            Float = f;
        }

        public void Set(IncomingMessage message)
        {
            Float = message.ReadFloat();
        }

        public byte[] ToBytes()
        {
            var data = new OutgoingMessage();

            data.Write(Float);

            return data.Data.ToArray();
        }

        public static implicit operator float(FloatPackage floatPackage)
        {
            return floatPackage.Float;
        }

        public static implicit operator FloatPackage(float f)
        {
            return new FloatPackage(f);
        }
    }
}
