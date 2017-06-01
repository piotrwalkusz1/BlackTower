using UnityEngine;
using System.Collections;
using NetworkProject.Combat;

public class BulletInfo
{
    public Bullet Bullet { get; set; }
    public float LiveTime { get; set; }
    public Vector3 Position { get; set; }
    public Quaternion Rotation { get; set; }
    public AttackInfo AttackInfo { get; set; }

    public BulletInfo(Bullet bullet, float liveTime, Vector3 position, Quaternion rotation, AttackInfo attackInfo)
    {
        Bullet = bullet;
        LiveTime = liveTime;
        Position = position;
        Rotation = rotation;
        AttackInfo = attackInfo;
    }

}
