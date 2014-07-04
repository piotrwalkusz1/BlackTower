using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NetworkProject
{
    public class BulletPackage : ObjectPackage
    {
        public Vector3 _position;
        public Vector3 _direction;
        public float _speed;

        public override void Set(IncomingMessage message)
        {
            base.Set(message);

            _position = message.ReadVector3();
            _direction = message.ReadVector3();
            _speed = message.ReadFloat();
        }

        public override byte[] ToBytes()
        {
            byte[] bytes = base.ToBytes();

            OutgoingMessage data = new OutgoingMessage();

            data.Write(bytes);

            data.Write(_position);
            data.Write(_direction);
            data.Write(_speed);
            
            return data.Data.ToArray();
        }
    }
}
