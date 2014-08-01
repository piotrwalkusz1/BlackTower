using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject
{
    public static class Settings
    {
        public const char messageSeparator1 = ';';
        public const int maxCharactersInAccount = 8;
        public const int mapsNumber = 1;       
        public const string serverIp = "127.0.0.1";
        public const int serverPort = 21325;
        public const int widthEquipment = 6;
        public const int heightEquipment = 8;
        public const float pickItemRange = 1.5f;
        public const float gravitation = 10f;
        public const float basicPlayerMovementSpeed = 4;
        public const int basicPlayerMaxHP = 100;
        public const int basicPlayerMaxExp = 100;
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
