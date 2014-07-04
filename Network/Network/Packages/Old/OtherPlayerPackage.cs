using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NetworkProject
{
    public class OtherPlayerPackage : PlayerPackage
    {
        public float Rotation { get; set; }
        public OtherPlayerStatsPackage Stats { get; set; }

        public override void Set(IncomingMessage message)
        {
            base.Set(message);
            Rotation = message.ReadFloat();
            Stats = message.Read<OtherPlayerStatsPackage>();
        }

        public override byte[] ToBytes()
        {
            byte[] bytes = base.ToBytes();

            OutgoingMessage message = new OutgoingMessage();

            message.Write(bytes);
            message.Write(Rotation);
            message.Write(Stats);

            return message.Data.ToArray();
        }
    }
}
