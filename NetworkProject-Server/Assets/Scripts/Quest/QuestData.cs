using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class QuestData
{
    public int IdQuest { get; set; }
    public List<IQuestTargetData> Targets { get; protected set; }
    public List<IRequirement> Requirements { get; protected set; }
    public List<IReward> Rewards { get; protected set; }
    public List<int> RequiredCompletedQuests { get; protected set; }

    public QuestData(int idQuest, IQuestTargetData[] targets, IRequirement[] requirements, IReward[] rewards, int[] requiredCompletedQuests)
    {
        IdQuest = idQuest;
        Targets = new List<IQuestTargetData>(targets);
        Requirements = new List<IRequirement>(requirements);
        Rewards = new List<IReward>(rewards);
        RequiredCompletedQuests = new List<int>(requiredCompletedQuests);
    }

    public List<IQuestTarget> GetExecutedVersionTargets(Quest quest)
    {
        var result = new List<IQuestTarget>();

        foreach (var target in Targets)
        {
            result.Add(target.GetExecutedVersionTarget(quest));
        }

        return result;
    }

    public void GiveRevard(IRewardable recipient)
    {
        Rewards.ForEach(x => x.GiveReward(recipient));
    }

    public void SelectChangesByRewards(RewardSenderUpdate sender)
    {
        Rewards.ForEach(x => x.SelectChange(sender));
    }

    public bool AreRequirementsSatisfy(PlayerStats stats)
    {
        return !Requirements.Exists(x => !x.IsRequirementSatisfy(stats));
    }

    public bool AreAllRequiredCompletedQuest(List<int> completedQuest)
    {
        foreach (var questId in RequiredCompletedQuests)
        {
            if (!completedQuest.Exists(x => x == questId))
            {
                return false;
            }
        }

        return true;
    }
}
