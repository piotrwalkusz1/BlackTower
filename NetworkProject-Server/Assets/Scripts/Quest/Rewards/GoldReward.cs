using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Quests;

public class GoldReward : IReward
{
    public int Gold { get; set; }

    public GoldReward(int gold)
    {
        Gold = gold;
    }

    public void GiveReward(IRewardable recipient)
    {
        recipient.AddGold(Gold);
    }

    public void SelectChange(RewardSenderUpdate sender)
    {
        sender.IsGold = true;
    }

    public IRewardPackage ToPackage()
    {
        return new GoldRewardPackage(Gold);
    }
}
