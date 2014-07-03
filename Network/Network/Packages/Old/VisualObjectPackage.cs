using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NetworkProject
{
    public class VisualObjectPackage : ObjectPackage
    {
        public VisualObjectType _objectType;
        public Vector3 _position;
        public float _rotation;

        public override void Set(IncomingMessage message)
        {
            base.Set(message);

            _objectType = (VisualObjectType)message.ReadInt();
            _position = message.ReadVector3();
            _rotation = message.ReadFloat();
        }

        public override byte[] ToBytes()
        {
            var data = new OutgoingMessage();

            byte[] bytes = base.ToBytes();

            data.Write(bytes);

            data.Write((int)_objectType);
            data.Write(_position);
            data.Write(_rotation);

            return data.Data.ToArray();
        }
    }
}
