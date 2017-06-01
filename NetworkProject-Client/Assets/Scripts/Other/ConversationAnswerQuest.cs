using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject.Connection.ToServer;

[Serializable]
public class ConversationAnswerQuest : ConversationAnswer
{
    public int QuestId { get; set; }

    public ConversationAnswerQuest(int idDialog, int idNextConversation, int questId)
        : base(idDialog, idNextConversation)
    {
        QuestId = questId;
    }

    public override void GiveAnswer()
    {
        base.GiveAnswer();

        //var request = new AskQuestProposalToServer(QuestId, GUIController.CurrentInterlocutor.GetComponent<NetObject>().IdNet);

        //Client.SendRequestAsMessage(request);

        var request = new TakeQuestToServer(QuestId, GUIController.CurrentInterlocutor.GetComponent<NetObject>().IdNet);

        Client.SendRequestAsMessage(request);
    }

    public override bool IsShowAnswer()
    {
        return QuestRepository.CanTakeQuest(QuestId);
    }
}
