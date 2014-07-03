using UnityEngine;
using System.Collections;
using System.Net;
using Lidgren.Network;
using NetworkProject;

[System.CLSCompliant(false)]
public class ConnectionMember : IConnectionMember
{
    public NetConnection NetAddress { get; set; }
    public IPEndPoint RemoteEndPoint
    {
        get
        {
            return NetAddress.RemoteEndPoint;
        }
    }

    public bool Equals(IConnectionMember conectionMember)
    {
        if (RemoteEndPoint.Equals(conectionMember.RemoteEndPoint))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
