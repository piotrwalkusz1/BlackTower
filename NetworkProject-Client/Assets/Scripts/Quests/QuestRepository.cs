using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class QuestRepository
{
    public static List<Quest> _quests;

    public static Quest GetQuest(int idQuest)
    {
        return _quests.First(x => x.IdQuest == idQuest);
    }

    public static List<int> GetReturnedQuestsId()
    {
        var returnedQuestsId = from quest in _quests
                               where quest.Status == QuestStatus.Returned
                               select quest.IdQuest;

        return returnedQuestsId.ToList();
    }

    public static Quest[] GetCurrentQuests()
    {
        return _quests.Where(x => x.Status == QuestStatus.InProgress || x.Status == QuestStatus.Completed).Select(x => x).ToArray();
    }

    public static bool CanTakeQuest(int idQuest)
    {
        Quest quest = GetQuest(idQuest);

        return quest.Status == QuestStatus.NoTaken &&
            quest.AreAllRequiredCompletedQuest(GetReturnedQuestsId());
    }
}
