using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Connection;

public enum ServerStatus
{
    Disconnected,
    Connecting,
    Connected,
    Disconnecting
}

[System.CLSCompliant(false)]
public interface IServer
{
    ServerStatus Status { get; }

    void Start(ServerConfig config);
    void Send(OutgoingMessage message, IConnectionMember address);
    IncomingMessage ReadMessage();
    void Close();
}

