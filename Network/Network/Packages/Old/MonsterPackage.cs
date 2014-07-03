using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NetworkProject
{
    public class MonsterPackage : ObjectPackage
    {
        public MonsterType _monsterType;
        public Vector3 _position;
        public float _rotation;
        public MonsterStatsPackage _stats;

        public override void Set(IncomingMessage message)
        {
            base.Set(message);

            _monsterType = (MonsterType)message.ReadInt();
            _position = message.ReadVector3();
            _rotation = message.ReadFloat();
            _stats = message.Read<MonsterStatsPackage>();
        }

        public override byte[] ToBytes()
        {
            byte[] bytes = base.ToBytes();

            OutgoingMessage data = new OutgoingMessage();

            data.Write(bytes);

            data.Write((int)_monsterType);
            data.Write(_position);
            data.Write(_rotation);
            data.Write(_stats);

            return data.Data.ToArray();
        }
    }
}
