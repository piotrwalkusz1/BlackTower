using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Standard;
using NetworkProject.Quests;

public class Quest
{
    public int IdQuest { get; protected set; }
    public int NetIdQuester { get; protected set; }
    public string Name
    {
        get { return Languages.GetQuestName(IdQuest); }
    }
    public string Description
    {
        get { return Languages.GetQuestDescription(IdQuest); }
    }
    public QuestStatus Status { get; set; }

    protected List<IQuestTarget> _targets;
    protected List<IReward> _rewards;
    protected List<int> _requiredCompletedQuests;

    public Quest(int idQuest, int netIdQuester, QuestStatus status, IQuestTarget[] targets, IReward[] rewards, int[] requiredCompletedQuests)
    {
        IdQuest = idQuest;
        NetIdQuester = netIdQuester;
        Status = status;
        _targets = new List<IQuestTarget>(targets);
        _rewards = new List<IReward>(rewards);
        _requiredCompletedQuests = new List<int>(requiredCompletedQuests);
    }

    public IQuestTarget[] GetTargets()
    {
        return _targets.ToArray();
    }

    public IReward[] GetRewards()
    {
        return _rewards.ToArray();
    }

    public string GetTargetsDescription()
    {
        if (Status == QuestStatus.Completed)
        {
            return Languages.GetPhrase("questCompleted");
        }
        else
        {
            List<string> descriptions = new List<string>();

            foreach (var target in _targets.Cast<IQuestTarget>())
            {
                descriptions.Add(target.GetDescription());
            }

            string result = "";

            for (int i = 0; i < descriptions.Count; i++)
            {
                if (i != 0)
                {
                    result += '\n';
                }

                result += descriptions[i];
            }

            return result;
        }
    }

    public bool AreAllRequiredCompletedQuest(List<int> completedQuest)
    {
        foreach (var questId in _requiredCompletedQuests)
        {
            if (!completedQuest.Exists(x => x == questId))
            {
                return false;
            }
        }

        return true;
    }

    public void DeleteQuest()
    {
        Status = QuestStatus.NoTaken;

        ResetTargets();
    }

    public void ResetTargets()
    {
        _targets.ForEach(x => x.Reset());
    }
}
