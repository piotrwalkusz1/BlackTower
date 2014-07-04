using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Lidgren.Network;
using NetworkProject.Connection;
using UnityEngine;

[System.CLSCompliant(false)]
public class LidgrenServer : IServer
{
    public ServerStatus Status
    {
        get
        {
            if (_netServer == null)
            {
                return ServerStatus.Disconnected;
            }
            switch (_netServer.Status)
            {
                case NetPeerStatus.NotRunning:
                    return ServerStatus.Disconnected;
                case NetPeerStatus.Starting:
                    return ServerStatus.Connecting;
                case NetPeerStatus.Running:
                    return ServerStatus.Connected;
                case NetPeerStatus.ShutdownRequested:
                    return ServerStatus.Disconnecting;
                default:
                    return ServerStatus.Disconnecting;
            }
        }
    }

    private NetServer _netServer;

    public void Start(ServerConfig config)
    {
        NetPeerConfiguration netConfig = new NetPeerConfiguration("NetworkProject");
        netConfig.MaximumConnections = config.MaxPlayers;
        netConfig.Port = config.Port;
        NetServer server = new NetServer(netConfig);
        server.Start();
        _netServer = server;
    }

    public void Send(OutgoingMessage message, IConnectionMember address)
    {
        NetConnection netAddress = ((ConnectionMember)address).NetAddress;
        NetOutgoingMessage netMessage = _netServer.CreateMessage();

        netMessage.Write(message.GetBytes());

        _netServer.SendMessage(netMessage, netAddress, NetDeliveryMethod.ReliableOrdered);
    }

    public IncomingMessage ReadMessage()
    {
        NetIncomingMessage netMessage;

        while((netMessage = _netServer.ReadMessage()) != null)
        {
            if (IsMessageUnimportant(netMessage))
            {           
                continue;
            }

            var address = new ConnectionMember(netMessage.SenderConnection);
            var message = new IncomingMessage(netMessage.Data, address);
            return message;
        }

        return null;
    }

    public void Close()
    {
        _netServer.Shutdown("");
    }

    private bool IsMessageUnimportant(NetIncomingMessage message)
    {
        return message.MessageType != NetIncomingMessageType.Data;
    }
}
