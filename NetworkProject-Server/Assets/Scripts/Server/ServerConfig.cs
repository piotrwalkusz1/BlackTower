using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[System.CLSCompliant(false)]
public class ServerConfig
{
    public int Port { get; set; }
    public int MaxPlayers { get; set; }

    public ServerConfig(int port, int maxPlayers = 100)
    {
        Port = port;
        MaxPlayers = maxPlayers;
    }
}
