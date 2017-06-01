using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Quests;

public class ExpReward : IReward
{
    public int Exp { get; set; }

    public ExpReward(int exp)
    {
        Exp = exp;
    }

    public void GiveReward(IRewardable recipient)
    {
        recipient.AddExp(Exp);
    }

    public IRewardPackage ToPackage()
    {
        return new ExpRewardPackage(Exp);
    }


    public void SelectChange(RewardSenderUpdate sender)
    {
        sender.IsStats = true;
    }
}
