using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject.Combat;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class CreateBulletToClient : CreateToClient
    {
        public override float Rotation
        {
            get
            {
                return FullRotation.y;
            }
            set
            {
                FullRotation = new Vector3(FullRotation.x, value, FullRotation.z);
            }
        }   
        public Vector3Serializable FullRotation { get; set; }
        public Bullet Bullet { get; set; }

        public CreateBulletToClient(int idNet, Vector3 position, Vector3 rotation, Bullet bullet)
            : base(idNet, position, rotation.y)
        {
            FullRotation = rotation;
            Bullet = bullet;
        }
    }
}
