using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;
using NetworkProject.Connection;

public enum ClientStatus
{
    Disconnected,
    Connecting,
    Connected,
    Disconnecting
}

public interface IClient
{
    ClientStatus Status { get; }

    void Start();
    void Connect(string host, int port);
    void Send(OutgoingMessage message);
    IncomingMessageFromServer ReadMessage();
    void Disconnect();
    void Close();
    bool IsRunning();
}

