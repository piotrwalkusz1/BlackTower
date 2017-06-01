using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Quests
{
    [Serializable]
    public class ExpRewardPackage : IRewardPackage
    {
        public int Exp { get; set; }

        public ExpRewardPackage(int exp)
        {
            Exp = exp;
        }
    }
}
