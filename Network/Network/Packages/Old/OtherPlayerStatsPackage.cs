using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject
{
    public class OtherPlayerStatsPackage : PlayerStatsPackage
    {
        public override void Set(IncomingMessage message)
        {
            base.Set(message);
        }

        public override byte[] ToBytes()
        {
            Byte[] bytes = base.ToBytes();

            var data = new OutgoingMessage();

            data.Write(bytes);

            return data.Data.ToArray();
        }
    }
}
