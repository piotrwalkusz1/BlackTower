using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;

public enum ClientStatus
{
    Disconnected,
    Connecting,
    Connected,
    Disconnecting
}

[System.CLSCompliant(false)]
public interface IClient
{
    ClientStatus Status { get; }

    void Start(ClientConfig config);
    void Connect(string host, int port);
    void Send(OutgoingMessage message);
    IncomingMessage ReadMessage();
    void Disconnect();
    void Close();
}

