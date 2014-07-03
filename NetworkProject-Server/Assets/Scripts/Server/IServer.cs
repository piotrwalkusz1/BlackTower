using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;

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
    void Send(OutgoingMessage message, IConnectionMember[] addresses);
    IncomingMessage ReadMessage();
    void Close();
}

