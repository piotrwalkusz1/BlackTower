using UnityEngine;
using System.Collections;
using NetworkProject;

[System.CLSCompliant(false)]
public class ServerController : MonoBehaviour
{
    void Awake()
    {
        var serverConfig = new ServerConfig(Settings.serverPort);
        Server.Start(serverConfig);
    }

    void Update()
    {
        Server.Listen();
    }
}
