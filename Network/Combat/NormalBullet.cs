using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject.Movement;

namespace NetworkProject.Combat
{
    [Serializable]
    public class NormalBullet : Bullet
    {
        private float _speed;

        public NormalBullet(float speed)
        {
            _speed = speed;
        }

        public override GameObject CreateInstantiate(Vector3 position, Quaternion rotation)
        {
            GameObject instantiate = GameObject.Instantiate(Bullets.GetBullet(BulletType), position, rotation) as GameObject;
            
            instantiate.GetComponent<SimpleMovement>().Speed = _speed;

            return instantiate;
        }
    }
}
