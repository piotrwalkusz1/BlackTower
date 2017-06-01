using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Standard;
using NetworkProject.Connection.ToServer;

[Serializable]
public class ConversationAnswerReturnQuest : ConversationAnswer
{
    public int QuestId { get; set; }

    public ConversationAnswerReturnQuest(int idDialog, int idNextConversation, int questId)
        : base(idDialog, idNextConversation)
	{
        QuestId = questId;
	}

    public override void GiveAnswer()
    {
        int idNetQuester = GUIController.CurrentInterlocutor.GetComponent<NetObject>().IdNet;

        if(!Client.GetNetOwnPlayer().GetComponent<OwnPlayerEquipment>().CanTakeRewardForQuest(QuestId))
        {
            GUIController.ShowMessage(Languages.GetPhrase("equipmentIsOverflowing"));

            GUIController.HideDialogWindow();

            return;
        }

        base.GiveAnswer();

        var request = new ReturnQuestToServer(QuestId, idNetQuester);

        Client.SendRequestAsMessage(request);
    }

    public override bool IsShowAnswer()
    {
        var quest = QuestRepository.GetQuest(QuestId);

        return quest != null && quest.Status == QuestStatus.Completed;
    }
}
