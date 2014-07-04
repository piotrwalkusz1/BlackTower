using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class CreateBullet : Create
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
        public Vector3 FullRotation;
        public int IdBullet;
        public float Speed { get; set; }

        public CreateBullet(int idNet, Vector3 position, Vector3 rotation, int idBullet, float speed)
        {
            IdNet = idNet;
            Position = position;
            FullRotation = rotation;
            IdBullet = idBullet;
            Speed = speed;
        }
    }
}
