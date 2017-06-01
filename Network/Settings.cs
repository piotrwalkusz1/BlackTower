using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject
{
    public static class Settings
    {
        public const string gameName = "NetworkProject-1273";
        public const char messageSeparator1 = ';';
        public const int maxCharactersInAccount = 8;
        public const int mapsNumber = 1;       
        public const string serverIp = "127.0.0.1";
        public const int serverPort = 21325;
        public const int widthEquipment = 6;
        public const int heightEquipment = 8;
        public const float pickItemRange = 1.5f;
        public const float talkNPCRange = 8;
        public const float gravitation = 10f;
        public const float basicPlayerMovementSpeed = 6;
        public const int basicPlayerMaxHP = 100;
        public const int basicPlayerMaxExp = 100;
        public const int basicPlayerMaxMana = 100;
        public const float basicHPRegeneration = 1f;
        public const float basicManaRegeneration = 1f;
        public const int additionalMaxExpPerLvl = 5;

        public static int GetMaxExpInLvl(int lvl)
        {
            return basicPlayerMaxExp + (lvl - 1) * additionalMaxExpPerLvl;
        }

        public static float GetTimeBetweenAttacks(int attackSpeed)
        {
            int speed = attackSpeed >= 25 ? attackSpeed : 25;

            return 50f / speed;
        }
    }
}
