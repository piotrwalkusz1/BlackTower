using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Quests
{
    public interface IRewardable
    {
        void AddGold(int gold);

        void AddExp(int exp);
    }
}
