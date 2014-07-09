using UnityEngine;
using System.Collections;
using NetworkProject;
using NetworkProject.Connection;

public class BufferedMessage : IncomingMessageFromServer
{
    public int _time;

    public BufferedMessage(IncomingMessageFromServer message)
        : base(message)
    {
        _time = 0;
    }
}
