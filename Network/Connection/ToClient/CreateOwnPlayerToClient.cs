using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject.Items;
using NetworkProject.Spells;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class CreateOwnPlayerToClient : CreateToClient
    {
        public PlayerStatsPackage PlayerStats { get; set; }
        public string Name { get; set; }
        public Equipment Equipment { get; set; }
        public PlayerEquipedItems EquipedItems { get; set; }
        public List<Spell> Spells { get; set; }

        public CreateOwnPlayerToClient(int idNet, bool isModelVisible, Vector3 position, float rotation, IPlayerStats playerStats, string name,
            Equipment equipment, PlayerEquipedItems equipedItems, List<Spell> spells)
        {
            IdNet = idNet;
            IsModelVisible = isModelVisible;
            PlayerStats = (PlayerStatsPackage)StatsPackage.GetStatsPackage(playerStats);
            Position = position;
            Rotation = rotation;
            Name = name;
            Equipment = equipment;
            EquipedItems = equipedItems;
            Spells = spells;
        }
    }
}
