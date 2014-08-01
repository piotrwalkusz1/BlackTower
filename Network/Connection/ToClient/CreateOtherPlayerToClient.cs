using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject.Items;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class CreateOtherPlayerToClient : CreateToClient
    {
        public PlayerStatsPackage PlayerStats { get; set; }
        public string Name { get; set; }
        public PlayerEquipedItems EquipedItems { get; set; }

        public CreateOtherPlayerToClient(int idNet, bool isModelVisible, Vector3 position, float rotation, IPlayerStats playerStats, string name,
            PlayerEquipedItems equipedItems)
        {
            IdNet = idNet;
            IsModelVisible = isModelVisible;
            PlayerStats = (PlayerStatsPackage)StatsPackage.GetStatsPackage(playerStats);
            Position = position;
            Rotation = rotation;
            Name = name;
            EquipedItems = equipedItems;
        }
    }
}
