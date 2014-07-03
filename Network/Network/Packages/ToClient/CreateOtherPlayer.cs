using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NetworkProject.Packages.ToClient
{
    [Serializable]
    public class CreateOtherPlayer : Create
    {
        public Stats PlayerStats { get; set; }

        public CreateOtherPlayer(int idNet, Vector3 position, float rotation, Stats playerStats)
        {
            IdNet = idNet;
            PlayerStats = playerStats;
            Position = position;
            Rotation = rotation;
        }
    }
}
