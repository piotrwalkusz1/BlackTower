using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Quests
{
    public interface IReward
    {
        void GiveReward(IRewardable recipient);
    }
}
