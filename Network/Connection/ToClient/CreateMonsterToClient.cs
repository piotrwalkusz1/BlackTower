using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class CreateMonsterToClient : CreateToClient
    {
        public int IdMonster { get; set; }
        public IMonsterStats MonsterStats { get; set; }

        public CreateMonsterToClient(int idNet, Vector3 position, float rotation, int idMonster, IMonsterStats monsterStats)
        {
            IdNet = idNet;
            Position = position;
            Rotation = rotation;
            IdMonster = idMonster;
            MonsterStats = (IMonsterStats)Properter.GetProperter(MonsterStats);
        }
    }
}
