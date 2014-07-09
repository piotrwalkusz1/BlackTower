using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class CreateOtherPlayerToClient : CreateToClient
    {
        public IPlayerStats PlayerStats { get; set; }
        public string Name { get; set; }

        public CreateOtherPlayerToClient(int idNet, Vector3 position, float rotation, IPlayerStats playerStats, string name)
        {
            IdNet = idNet;
            PlayerStats = (IPlayerStats)Properter.GetProperter(playerStats);
            Position = position;
            Rotation = rotation;
            Name = name;
        }
    }
}
