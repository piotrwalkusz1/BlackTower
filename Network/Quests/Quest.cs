using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Quests
{
    public class Quest
    {
        protected List<IQuestTarget> _targets;

        protected List<IReward> _rewards;

        public Quest(IQuestTarget[] targets, IReward[] rewards)
        {
            _targets = new List<IQuestTarget>(targets);
            _rewards = new List<IReward>(rewards);
        }

        public bool IsComplete()
        {
            foreach (var target in _targets)
            {
                if (!target.IsComplete())
                {
                    return false;
                }
            }

            return true;
        }

        public void GiveReward(IRewardable recipient)
        {
            foreach (var reward in _rewards)
            {
                reward.GiveReward(recipient);
            }
        }
    }
}
