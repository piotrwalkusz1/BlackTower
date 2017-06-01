using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Chat
{
    public bool IsWritte { get; set; }
    public string NewMessage { get; set; }
    private List<string> _messages;
    public const int MAX_CHAR_IN_MESSAGE = 80;
    private const int MAX_MESSAGES_IN_CHAT = 10;

    public Chat()
    {
        NewMessage = "";

        _messages = new List<string>();

        for (int i = 0; i < MAX_MESSAGES_IN_CHAT; i++)
        {
            _messages.Add("");
        }
    }

    public void AddMessage(string message)
    {
        List<string> newMessages = new List<string>();

        newMessages.Add(message);

        RemoveLastMessage(_messages);

        newMessages.AddRange(_messages);

        _messages = newMessages;
    }

    public string[] GetMessages()
    {
        return _messages.ToArray();
    }

    private void RemoveLastMessage(List<string> messagesList)
    {
        messagesList.RemoveAt(MAX_MESSAGES_IN_CHAT - 1);
    }
}
