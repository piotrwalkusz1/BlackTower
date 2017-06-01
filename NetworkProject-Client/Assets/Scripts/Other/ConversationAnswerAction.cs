using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Connection.ToServer;

[Serializable]
public class ConversationAnswerAction : ConversationAnswer
{
    public int ActionId { get; set; }

    public ConversationAnswerAction(int dialogId, int nextConversationId, int actionId)
        : base(dialogId, nextConversationId)
    {
        ActionId = actionId;
    }

    public override void GiveAnswer()
    {
        base.GiveAnswer();

        var request = new ExecuteActionToServer(GUIController.CurrentInterlocutor.GetComponent<NetObject>().IdNet, ActionId);

        Client.SendRequestAsMessage(request);
    }
}
