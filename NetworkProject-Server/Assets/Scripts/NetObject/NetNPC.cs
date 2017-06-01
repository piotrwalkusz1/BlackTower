using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject.Connection;
using NetworkProject.Connection.ToClient;

public class NetNPC : NetObject
{
    public int _idNPC;
    public int _modelId;

    protected new void Awake()
    {
        base.Awake();

        IdNet = SceneBuilder.GetNextIdNet();
    }

    public override void SendMessageAppeared(IConnectionMember address)
    {
        var request = new CreateNPCToClient(IdNet, transform.position, transform.eulerAngles.y, _idNPC, _modelId);

        Server.SendRequestAsMessage(request, address);
    }
}
