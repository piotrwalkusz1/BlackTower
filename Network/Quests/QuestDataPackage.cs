using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Requirements;

namespace NetworkProject.Quests
{
    [Serializable]
    public class QuestDataPackage
    {
        public int IdQuest { get; set; }
        public int NetIdQuester { get; set; }
        public QuestStatusPackage QuestStatus { get; set; }
        protected List<IQuestTargetPackage> _targets;
        protected List<IRewardPackage> _rewards;
        protected List<int> _requiredCompletedQuests;

        public QuestDataPackage(int idQuest, int netIdQuester, QuestStatusPackage questStatus, IQuestTargetPackage[] targets, IRewardPackage[] rewards, int[] requiredCompletedQuests)
        {
            IdQuest = idQuest;
            NetIdQuester = netIdQuester;
            QuestStatus = questStatus;
            _targets = new List<IQuestTargetPackage>(targets);
            _rewards = new List<IRewardPackage>(rewards);
            _requiredCompletedQuests = new List<int>(requiredCompletedQuests);
        }

        public IQuestTargetPackage[] GetTargets()
        {
            return _targets.ToArray();
        }

        public IRewardPackage[] GetRewards()
        {
            return _rewards.ToArray();
        }

        public int[] GetRequiredCompletedQuests()
        {
            return _requiredCompletedQuests.ToArray();
        }

        public void SetTargets(IQuestTargetPackage[] targets)
        {
            _targets = new List<IQuestTargetPackage>(targets);
        }
    }
}
