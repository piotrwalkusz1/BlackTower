using UnityEngine;
using System.Collections;
using System.Net;
using Lidgren.Network;
using NetworkProject.Connection;

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

    public ConnectionMember(NetConnection netAddress)
    {
        NetAddress = netAddress;
    }

    public bool Equals(IConnectionMember conectionMember)
    {
        return RemoteEndPoint.Equals(conectionMember.RemoteEndPoint);
    }
}
