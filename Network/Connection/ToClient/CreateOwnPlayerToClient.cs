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
        public EquipmentPackage Equipment { get; set; }
        public PlayerEquipedItemsPackage EquipedItems { get; set; }
        public List<SpellPackage> Spells { get; set; }

        public CreateOwnPlayerToClient(int idNet, bool isModelVisible, Vector3 position, float rotation, IPlayerStats playerStats, string name,
            EquipmentPackage equipment, PlayerEquipedItemsPackage equipedItems, List<SpellPackage> spells)
            : base(idNet, isModelVisible, position, rotation)
        {
            PlayerStats = (PlayerStatsPackage)StatsPackage.GetStatsPackage(playerStats);
            Name = name;
            Equipment = equipment;
            EquipedItems = equipedItems;
            Spells = spells;
        }
    }
}
