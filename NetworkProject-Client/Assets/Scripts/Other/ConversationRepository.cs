using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using Standard;

[Serializable]
public static class ConversationRepository
{
    private static List<Conversation> _conversations = new List<Conversation>();

    static ConversationRepository()
    {
        // Test

        /*_conversations.Add(new Conversation(
            0,
            2,
            new ConversationAnswerIfCanTakeQuest(0, 1, 0),
            new ConversationAnswerReturnQuest(5, 2, 0),
            new ConversationAnswer(4, -1)));

        _conversations.Add(new Conversation(
            1,
            3,
            new ConversationAnswerQuest(1, -1, 0),
            new ConversationAnswer(4, -1)));

        _conversations.Add(new Conversation(
            2,
            6,
            new ConversationAnswer(4, -1)));

        // Map1 : fisher

        _conversations.Add(new Conversation(
            100,
            7,
            new ConversationAnswer(8, 101),
            new ConversationAnswer(4, -1)));

        _conversations.Add(new Conversation(
            101,
            9,
            new ConversationAnswer(4, -1)));

        // Map1 : Gold guard

        _conversations.Add(new Conversation( 200, 10,
            new ConversationAnswerIfCanTakeQuest(11, 201, 1),
            new ConversationAnswer(4, -1)));

        _conversations.Add(new Conversation(201, 12,
            new ConversationAnswerQuest(13, -1, 1),
            new ConversationAnswer(14, -1)));

        // Map1 : Guard instructor

        _conversations.Add(new Conversation(300, 15,
            new ConversationAnswerReturnQuest(16, 301, 1),
            new ConversationAnswerReturnQuest(22, 302, 2),
            new ConversationAnswerIfCanTakeQuest(20, 303, 2),
            new ConversationAnswerIfCanTakeQuest(24, 304, 3),
            new ConversationAnswer(4, -1)));

        _conversations.Add(new Conversation(301, 17,
            new ConversationAnswerAction(18, 305, 0),
            new ConversationAnswer(14, -1)));

        _conversations.Add(new Conversation(302,

        _conversations.Add(new Conversation(303,

        _conversations.Add(new Conversation(304,

        _conversations.Add(new Conversation(305,

        _conversations.Add(new Conversation(306,
         */
    }

    public static Conversation GetConversation(int idConversation)
    {
        return _conversations.First(x => x.IdConversation == idConversation);
    }

    public static void LoadAndSetConversations()
    {
        var data = Resources.Load(Settings.pathToConversationsInResources, typeof(TextAsset)) as TextAsset;

        var stream = new MemoryStream(data.bytes);

        var formatter = new BinaryFormatter();

        _conversations = new List<Conversation>((Conversation[])formatter.Deserialize(stream));
    }
}
