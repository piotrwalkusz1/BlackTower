using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject
{
    public class PlayerStatsPackage : StatsPackage
    {
        public int _hp;
        public int _maxHp;
        public float _hpRegeneration;
        public int _defense;
        public int _minDmg;
        public int _maxDmg;
        public float _attackSpeed;
        public float _movementSpeed;
        public Breed _breed;

        public override void Set(IncomingMessage message)
        {
            _hp = message.ReadInt();
            _maxHp = message.ReadInt();
            _hpRegeneration = message.ReadFloat();
            _defense = message.ReadInt();
            _minDmg = message.ReadInt();
            _maxDmg = message.ReadInt();
            _attackSpeed = message.ReadFloat();
            _movementSpeed = message.ReadFloat();
            _breed = (Breed)message.ReadInt();
        }

        public override byte[] ToBytes()
        {
            var data = new OutgoingMessage();

            data.Write(_hp);
            data.Write(_maxHp);
            data.Write(_hpRegeneration);
            data.Write(_defense);
            data.Write(_minDmg);
            data.Write(_maxDmg);
            data.Write(_attackSpeed);
            data.Write(_movementSpeed);
            data.Write((int)_breed);

            return data.Data.ToArray();
        }
    }
}
