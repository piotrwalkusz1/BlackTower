using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NetworkProject
{
    public class PlayerPackage : NamedObjectPackage
    {
        public Vector3 Position { get; set; }

        public override void Set(IncomingMessage message)
        {
            base.Set(message);

            Position = message.ReadVector3();
        }

        public override byte[] ToBytes()
        {
            byte[] bytes = base.ToBytes();

            OutgoingMessage data = new OutgoingMessage();

            data.Write(bytes);

            data.Write(Position);

            return data.Data.ToArray();
        }
    }
}
