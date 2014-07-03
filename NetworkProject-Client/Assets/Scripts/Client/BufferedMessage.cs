using UnityEngine;
using System.Collections;
using NetworkProject;

[System.CLSCompliant(false)]
public class BufferedMessage : IncomingMessage
{
    public int _time;

    public BufferedMessage(IncomingMessage message) : base(message)
    {
        _time = 0;
    }
}
