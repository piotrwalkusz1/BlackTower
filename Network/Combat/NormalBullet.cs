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
        private float _lifeTime;
        private float _speed;

        public NormalBullet(float speed, float lifeTime)
        {
            _speed = speed;
            _lifeTime = lifeTime;
        }

        public override GameObject CreateInstantiate(Vector3 position, Quaternion rotation)
        {
            GameObject instantiate = GameObject.Instantiate(Bullets.GetBullet(BulletType), position, rotation) as GameObject;
            
            instantiate.GetComponent<SimpleMovement>().Speed = _speed;

            instantiate.GetComponent<Destroy>().RemainLifeTime = _lifeTime;

            return instantiate;
        }
    }
}
