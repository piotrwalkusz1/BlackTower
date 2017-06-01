using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class ConversationAnswerIfCanTakeQuest : ConversationAnswer
{
    public int QuestId { get; set; }

    public ConversationAnswerIfCanTakeQuest(int idDialog, int idNextConversation, int questId)
        : base(idDialog, idNextConversation)
    {
        QuestId = questId;
    }

    public override void GiveAnswer()
    {
        base.GiveAnswer();
    }

    public override bool IsShowAnswer()
    {
        return QuestRepository.CanTakeQuest(QuestId);
    }
}
