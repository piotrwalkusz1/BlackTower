using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Quests;

public interface IReward
{
    void GiveReward(IRewardable recipient);

    void SelectChange(RewardSenderUpdate sender);

    IRewardPackage ToPackage();
}
