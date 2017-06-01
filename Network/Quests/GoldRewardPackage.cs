using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Quests
{
    [Serializable]
    public class GoldRewardPackage : IRewardPackage
    {
        public int Gold { get; set; }

        public GoldRewardPackage(int gold)
        {
            Gold = gold;
        }
    }
}
