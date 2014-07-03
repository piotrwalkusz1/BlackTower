using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ClientConfig
{
    public int Port { get; set; }

    public ClientConfig(int port)
    {
        Port = port;
    }
}
