using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NetworkProject
{
    public class OwnPlayerPackage : PlayerPackage
    {
        public OwnPlayerStatsPackage Stats { get; set; }

        public override void Set(IncomingMessage message)
        {
            base.Set(message);

            Stats = message.Read<OwnPlayerStatsPackage>();
        }

        public override byte[] ToBytes()
        {
            byte[] bytes = base.ToBytes();

            OutgoingMessage data = new OutgoingMessage();

            data.Write(bytes);
            data.Write(Stats);

            return data.Data.ToArray();
        }
    }
}
