using UnityEngine;
using System.Collections;

[System.CLSCompliant(false)]
public class ClientController : MonoBehaviour
{
	void Start ()
    {
        ClientConfig config = new ClientConfig(0);
        Client.Start(config);
        Client.Connect("127.0.0.1", NetworkProject.Settings.serverPort);
	}
	
	void Update ()
    {
        BufferMessages.Update();
        Client.Listen();
	}
}
