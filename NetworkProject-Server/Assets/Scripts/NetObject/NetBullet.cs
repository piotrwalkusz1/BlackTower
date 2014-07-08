using UnityEngine;
using System.Collections;
using NetworkProject;
using NetworkProject.Connection;
using NetworkProject.Connection.ToClient;
using NetworkProject.Combat;

[System.CLSCompliant(false)]
public class NetBullet : NetObject
{
    public override void SendMessageAppeared(IConnectionMember address)
    {
        var bulletManager = GetComponent<BulletManager>();
        var request = new CreateBulletToClient(IdNet, transform.position, transform.eulerAngles, bulletManager.Bullet);
        var message = new OutgoingMessage(request);

        Server.Send(message, address);
    }

    public override void SendMessageUpdate(IConnectionMember address)
    {
        // don`t send updates because client will calculate position itself
    }
}
