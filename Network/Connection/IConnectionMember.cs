﻿
using System.Collections;
using System.Net;

namespace NetworkProject.Connection
{
    public interface IConnectionMember
    {
        IPEndPoint RemoteEndPoint { get; }
        bool Equals(IConnectionMember connectionMember);
    }
}

