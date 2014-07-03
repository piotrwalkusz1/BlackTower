using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using NetworkProject;

[System.CLSCompliant(false)]
static public class BufferMessages
{
    private const int _maxNumberDelays = 5;

    private static List<IncomingMessage> _messages;

    static BufferMessages()
    {
        _messages = new List<IncomingMessage>();
    }

    public static void DelayExecutionMessage(IncomingMessage message)
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
            }
        }

        message.SetIndex(0);

        BufferedMessage newMessage = new BufferedMessage(message);

        _messages.Add(newMessage);
    }

    public static void Update()
    {
        IncomingMessage[] messagesArray = _messages.ToArray();

        _messages.Clear();

        foreach (BufferedMessage message in messagesArray)
        {
            Client.ExecuteMessage(message);
        }
    }
}
