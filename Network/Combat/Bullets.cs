using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NetworkProject.Combat
{
    public class Bullets : MonoBehaviour
    {
        private static List<GameObject> _staticBullets;

        public List<GameObject> _bullets;

        void Awake()
        {
            _staticBullets = _bullets;
        }

        public static GameObject GetBullet(int bulletType)
        {
            return _staticBullets[bulletType];
        }
    }
}
