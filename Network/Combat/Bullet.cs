using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NetworkProject.Combat
{
    public abstract class Bullet
    {
        public int BulletType { get; set; }

        public abstract GameObject CreateInstantiate(Vector3 position, Quaternion rotation);
    }
}
