using UnityEngine;
using System.Collections;
using NetworkProject;
using NetworkProject.Connection;
using NetworkProject.Connection.ToClient;

public class NetVisualObject : NetObject
{
    public int _idVisualObject;

    public override void SendMessageAppeared(IConnectionMember address)
    {
        var request = new CreateVisualObjectToClient(IdNet, transform.position, transform.eulerAngles.y, _idVisualObject);

        Server.SendRequestAsMessage(request, address);
    }
}
