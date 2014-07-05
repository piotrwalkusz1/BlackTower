using UnityEngine;
using System.Collections;
using NetworkProject;
using NetworkProject.Connection;
using NetworkProject.Connection.ToClient;

[System.CLSCompliant(false)]
public class NetBullet : NetObject
{
    public float Speed { get; set; }
    public int BulletType { get; set; }

    public override void SendMessageAppeared(IConnectionMember address)
    {
        var request = new CreateBullet(IdNet, )
    }

    public override void SendMessageUpdate(IConnectionMember address)
    {
        // don`t send updates because client will calculate position itself
    }
}
