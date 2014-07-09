using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using NetworkProject;
using NetworkProject.Connection;

[System.CLSCompliant(false)]
static public class BufferMessages
{
    private const int _maxNumberDelays = 5;

    private static List<IncomingMessageFromServer> _messages;

    static BufferMessages()
    {
        _messages = new List<IncomingMessageFromServer>();
    }

    public static void DelayExecutionMessage(IncomingMessageFromServer message)
    {
        if (message is BufferedMessage)
        {
            BufferedMessage bufferedMessage = (BufferedMessage)message;

            if (bufferedMessage._time >= _maxNumberDelays)
            {
                return;
            }
            else
            {
                bufferedMessage._time++;

                _messages.Add(message);
            }
        }
        else
        {
            BufferedMessage newMessage = new BufferedMessage(message);

            _messages.Add(newMessage);
        } 
    }

    public static void Update()
    {
        IncomingMessageFromServer[] messagesArray = _messages.ToArray();

        _messages.Clear();

        foreach (BufferedMessage message in messagesArray)
        {
            Client.ExecuteMessage(message);
        }
    }
}
