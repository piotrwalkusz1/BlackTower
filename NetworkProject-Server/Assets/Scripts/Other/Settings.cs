﻿using UnityEngine;
using System.Collections;
using NetworkProject;

[assembly : System.CLSCompliant(false)]

namespace Standard
{
    public static class Settings
    {
        public static readonly string PATH_TO_MONSTER_IN_RESOURCE = "monsters";
        public static readonly string PATH_TO_ITEMS_IN_RESOURCE = "items";
        public static readonly string PATH_TO_SPELLS_IN_RESOURCES = "spells";
        public static readonly string PATH_TO_BUFFS_IN_RESOURCES = "buffs";

        public const float visionRange = 100f;
        public const int heightMap = 10000;
        public const float maxDropDistance = 2;
        public const float distanceBetweenDroppedItemAndGround = 0.5f;

        public static int GetMap(Vector3 position)
        {
            float a = position.y / heightMap;
            return Mathf.CeilToInt(a);
        }

        public static void StatsToDefault(IPlayerStats stats)
        {
            stats.MaxHP = NetworkProject.Settings.basicPlayerMaxExp;
            stats.HPRegeneration = NetworkProject.Settings.basicHPRegeneration;
            stats.MaxMana = NetworkProject.Settings.basicPlayerMaxMana;
            stats.ManaRegeneration = NetworkProject.Settings.basicManaRegeneration;
            stats.MovementSpeed = NetworkProject.Settings.basicPlayerMovementSpeed;
            stats.AttackSpeed = 0;
            stats.MinDmg = 0;
            stats.MaxDmg = 0;
            stats.Defense = 0;
        }
    }
}

