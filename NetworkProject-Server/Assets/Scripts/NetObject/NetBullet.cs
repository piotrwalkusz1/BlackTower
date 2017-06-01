using UnityEngine;
using System.Collections;
using NetworkProject;
using NetworkProject.Connection;
using NetworkProject.Connection.ToClient;
using NetworkProject.Combat;

public class NetBullet : NetObject
{
    public Bullet Bullet { get; set; }

    public int BulletType
    {
        get
        {
            return Bullet.BulletType;
        }
    }

    public override void SendMessageAppeared(IConnectionMember address)
    {
        var request = new CreateBulletToClient(IdNet, transform.position, transform.eulerAngles, Bullet);
        var message = new OutgoingMessage(request);

        Server.Send(message, address);
    }

    public override void SendMessageUpdate(IConnectionMember address)
    {
        // don`t send updates because client will calculate position itself
    }
}
