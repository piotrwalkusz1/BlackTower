using UnityEngine;
using System.Collections;
using NetworkProject.Combat;

[System.CLSCompliant(false)]
public class BulletInfo
{
    public Bullet Bullet { get; set; }
    public float LiveTime { get; set; }
    public Vector3 Position { get; set; }
    public Quaternion Rotation { get; set; }
    public AttackInfo AttackInfo { get; set; }
}
