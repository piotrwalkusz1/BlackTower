using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class CreateOwnPlayerToClient : CreateToClient
    {
        public IPlayerStats PlayerStats { get; set; }
        public string Name { get; set; }

        public CreateOwnPlayerToClient(int idNet, Vector3 position, float rotation, IPlayerStats playerStats, string name)
        {
            IdNet = idNet;
            PlayerStats = playerStats;
            Position = position;
            Rotation = rotation;
            Name = name;
        }
    }
}
