using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Lidgren.Network;
using NetworkProject;
using NetworkProject.Connection;
using UnityEngine;

public class 
    LidgrenClient : IClient
{
    public ClientStatus Status
    {
        get
        {
            if (_netClient == null)
            {
                return ClientStatus.Disconnected;
            }
            switch (_netClient.ConnectionStatus)
            {
                case NetConnectionStatus.Disconnected:
                    return ClientStatus.Disconnected;
                case NetConnectionStatus.Connected:
                    return ClientStatus.Connected;
                case NetConnectionStatus.Disconnecting:
                    return ClientStatus.Disconnecting;
                default:
                    return ClientStatus.Disconnected;
            }
        }
    }
    private NetConnection _serverAddress;

    private NetClient _netClient;

    public void Start()
    {
        NetPeerConfiguration netConfig = new NetPeerConfiguration("NP");
        NetClient client = new NetClient(netConfig);
        client.Start();
        _netClient = client;
    }

    public void Connect(string host, int port)
    {
        _serverAddress = _netClient.Connect(host, port);
    }

    public void Send(OutgoingMessage message)
    {
        NetOutgoingMessage netMessage = _netClient.CreateMessage();
        netMessage.Write(message.GetBytes());
        _netClient.SendMessage(netMessage, _serverAddress, NetDeliveryMethod.ReliableOrdered);
    }

    public IncomingMessageFromServer ReadMessage()
    {
        NetIncomingMessage m;
        while ((m = _netClient.ReadMessage()) != null)
        {
            if (m.MessageType != NetIncomingMessageType.Data)
            {
                continue;
            }

            if (m.SenderEndPoint.Address.Equals(_serverAddress.RemoteEndPoint.Address))
            {
                byte[] data = m.Data;
                var message = new IncomingMessageFromServer(data);
                return message;
            }
            else
            {
                continue;
            }
        }
        return null;
    }

    public void Disconnect()
    {
        _netClient.Disconnect("");
    }

    public void Close()
    {
        _netClient.Shutdown("");
    }

    public bool IsRunning()
    {
        return _netClient != null && _netClient.Status == NetPeerStatus.Running;
    }
}
