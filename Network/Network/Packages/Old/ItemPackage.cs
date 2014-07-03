using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NetworkProject
{
    public class ItemPackage : ObjectPackage
    {
        public int IdItem { get; set; }
        public Vector3 Position { get; set; }

        public override void Set(IncomingMessage message)
        {
            base.Set(message);

            IdItem = message.ReadInt();
            Position = message.ReadVector3();
        }

        public override byte[] ToBytes()
        {
            var data = new OutgoingMessage();

            data.Write(base.ToBytes());

            data.Write(IdItem);
            data.Write(Position);

            return data.Data.ToArray();
        }
    }
}
