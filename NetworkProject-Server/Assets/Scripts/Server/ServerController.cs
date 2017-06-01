using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NetworkProject;

public class ServerController : MonoBehaviour
{
    void Awake()
    {
        
    }

    void Update()
    {
        if (Server.Status == ServerStatus.Connected)
        {
            Server.Listen();
        }       
    }

    void OnGUI()
    {
        Server.OnGUI();
    }
}
