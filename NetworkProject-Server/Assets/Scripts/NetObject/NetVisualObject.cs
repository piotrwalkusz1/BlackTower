using UnityEngine;
using System.Collections;
using NetworkProject;

public class NetVisualObject : NetObject
{
    public VisualObjectType _modelType;

    new protected void Awake()
    {
        VisionFunctionToDefault();
    }

    new protected void LateUpdate()
    {

    }

    public override void SendMessageAppeared(NetworkProject.IConnectionMember address)
    {
        var package = new ObjectPackage();
        package.IdObject = IdObject;

        Server.SendMessageCreateVisualObject(package, (int)_modelType, transform.position, transform.eulerAngles.y, address);
    }

    public override void SendMessageUpdate(IConnectionMember address)
    {
        //empty
    }

    public override void SendMessageDisappeared(IConnectionMember address)
    {
        Server.SendMessageDeleteObject(IdObject, address);
    }
}
