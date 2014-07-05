﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class CreateMonster : Create
    {
        public int IdMonster { get; set; }
        public IStats MonsterStats { get; set; }

        public CreateMonster(int idNet, Vector3 position, float rotation, int idMonster, IStats monsterStats)
        {
            IdNet = idNet;
            Position = position;
            Rotation = rotation;
            IdMonster = idMonster;
            MonsterStats = monsterStats;
        }
    }
}
