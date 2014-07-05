using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class CreateOwnPlayer : Create
    {
        public IStats PlayerStats { get; set; }

        public CreateOwnPlayer(int idNet, Vector3 position, float rotation, IStats playerStats)
        {
            IdNet = idNet;
            PlayerStats = playerStats;
            Position = position;
            Rotation = rotation;
        }
    }
}
