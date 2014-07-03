using UnityEngine;
using System.Collections;
using NetworkProject;

[System.CLSCompliant(false)]
public class NetBullet : NetObject
{
    public float _speed;

    public override void SendMessageAppeared(IConnectionMember address)
    {
        BulletPackage bullet = new BulletPackage();

        bullet.IdObject = IdObject;

        bullet._position = transform.position;
        bullet._direction = transform.TransformDirection(Vector3.forward);
        bullet._speed = _speed;

        Server.SendMessageCreateBullet(bullet, address);
    }

    public override void SendMessageUpdate(IConnectionMember address)
    {
        // don`t send updates because client will calculate position itself
    }

    public override void SendMessageDisappeared(IConnectionMember address)
    {
        Server.SendMessageDeleteObject(IdObject, address);
    }
}
