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

    public int GetIdImage()
    {
        return 2;
    }
}
