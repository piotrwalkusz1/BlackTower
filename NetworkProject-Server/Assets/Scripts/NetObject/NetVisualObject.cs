using UnityEngine;
using System.Collections;
using NetworkProject;
using NetworkProject.Connection;
using NetworkProject.Connection.ToClient;

public class NetVisualObject : NetObject
{
    public int IdVisualObject { get; set; }

    public override void SendMessageAppeared(IConnectionMember address)
    {
        var request = new CreateVisualObjectToClient(IdNet, transform.position, transform.eulerAngles.y, IdVisualObject);

        Server.SendRequestAsMessage(request, address);
    }

    public override void SendMessageUpdate(IConnectionMember address)
    {
        //empty
    }
}
