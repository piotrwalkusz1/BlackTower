using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject.Combat;

public class BulletManager : MonoBehaviour
{
    public Bullet Bullet { get; set; }

    public int BulletType
    {
        get
        {
            return Bullet.BulletType;
        }
    }
}
