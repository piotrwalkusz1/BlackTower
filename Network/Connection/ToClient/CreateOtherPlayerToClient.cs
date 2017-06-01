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
        public PlayerEquipedItemsPackage EquipedItems { get; set; }

        public CreateOtherPlayerToClient(int idNet, Vector3 position, float rotation, IPlayerStats playerStats, string name,
            PlayerEquipedItemsPackage equipedItems)
            : base(idNet, position, rotation)
        {
            PlayerStats = (PlayerStatsPackage)StatsPackage.GetStatsPackage(playerStats);
            Name = name;
            EquipedItems = equipedItems;
        }

        public CreateOtherPlayerToClient(int idNet, bool isModelVisible, Vector3 position, float rotation, IPlayerStats playerStats, string name,
            PlayerEquipedItemsPackage equipedItems)
            : base(idNet, isModelVisible, position, rotation)
        {
            PlayerStats = (PlayerStatsPackage)StatsPackage.GetStatsPackage(playerStats);
            Name = name;
            EquipedItems = equipedItems;
        }
    }
}
