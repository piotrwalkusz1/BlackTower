using UnityEngine;
using System.Collections;
using NetworkProject;
using NetworkProject.Connection;
using NetworkProject.Connection.ToServer;

public class NetVisualObject : NetObject
{
    public VisualObjectType _modelType;

    public override void SendMessageAppeared(IConnectionMember address)
    {
        var request = new
    }

    public override void SendMessageUpdate(IConnectionMember address)
    {
        //empty
    }
}
