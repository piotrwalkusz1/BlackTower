using UnityEngine;
using System;
using System.Collections;
using NetworkProject.Connection.ToServer;

public class ClientController : MonoBehaviour
{
    private enum ConnectionStage
    {
        Disconnection,
        Connecting,
        Connected,
        Error
    }

    private static ConnectionStage _connectionStage;

    private static DateTime _endTimeConnection;

    private ClientStatus _lastStatus = ClientStatus.Disconnected;

    void Awake()
    {
        Client.Start();

        _connectionStage = ConnectionStage.Disconnection;
    }

	void Update ()
    {
        if (Client.Status == ClientStatus.Connected)
        {
            _connectionStage = ConnectionStage.Connected;
        }

        switch (_connectionStage)
        {
            case ConnectionStage.Disconnection:
            case ConnectionStage.Connecting:
                ConnectToServer();
                break;
        }

        if (Client.IsRunning())
        {
            BufferMessages.Update();
            Client.Listen();
        }

        if (Client.Status == ClientStatus.Connected && _lastStatus == ClientStatus.Disconnected)
        {
            _lastStatus = ClientStatus.Connected;
        }
        else if (Client.Status == ClientStatus.Disconnected && _lastStatus == ClientStatus.Connected)
        {
            _lastStatus = ClientStatus.Disconnected;
        }
	}

    public static bool IsError()
    {
        return _connectionStage == ConnectionStage.Error;
    }

    public static void Logout()
    {
        var request = new LogoutToServer();

        Client.SendRequestAsMessage(request);

        ApplicationControler.GoToLoginMenu();
    }

    private static void ConnectToServer()
    {
        if (_connectionStage == ConnectionStage.Disconnection)
        {
            if (Standard.Settings.UserConfiguration.IP != "")
            {
                Client.Connect(Standard.Settings.UserConfiguration.IP, Standard.Settings.UserConfiguration.Port);
                _connectionStage = ConnectionStage.Connecting;
                _endTimeConnection = DateTime.UtcNow.AddSeconds(5);
            }
            else
            {
                _connectionStage = ConnectionStage.Error;
            }
        }
        else if (_connectionStage == ConnectionStage.Connecting)
        {
            if (DateTime.UtcNow > _endTimeConnection)
            {
                _connectionStage = ConnectionStage.Error;
            }
        }
    }
}
