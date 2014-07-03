using UnityEngine;
using System.Collections;

namespace Standard
{
    [System.CLSCompliant(false)]
    static public class Settings
    {
        public const float visionRange = 100f;
        public const int heightMap = 10000;
        public const float maxDropDistance = 2;
        public const float distanceBetweenDroppedItemAndGround = 0.5f;

        public static int GetMap(Vector3 position)
        {
            float a = position.y / heightMap;
            return (int)Mathf.CeilToInt(a);
        }
    }
}

