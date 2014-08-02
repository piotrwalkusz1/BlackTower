using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Quests
{
    public class ExpReward : IReward
    {
        public int Exp { get; set; }

        public void GiveReward(IRewardable recipient)
        {
            recipient.AddExp(Exp);
        }
    }
}
