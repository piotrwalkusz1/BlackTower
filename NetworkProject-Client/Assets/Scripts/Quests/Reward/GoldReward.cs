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

    public int GetIdImage()
    {
        return 3;
    }
}
