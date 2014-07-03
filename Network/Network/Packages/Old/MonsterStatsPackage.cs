using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject
{
    public class MonsterStatsPackage : StatsPackage
    {
        public int _hp;
        public int _maxHp;
        public float _movementSpeed;

        public override void Set(IncomingMessage message)
        {
            _hp = message.ReadInt();
            _maxHp = message.ReadInt();
            _movementSpeed = message.ReadFloat();
        }

        public override byte[] ToBytes()
        {
            var data = new OutgoingMessage();

            data.Write(_hp);
            data.Write(_maxHp);
            data.Write(_movementSpeed);

            return data.Data.ToArray();
        }
    }
}
