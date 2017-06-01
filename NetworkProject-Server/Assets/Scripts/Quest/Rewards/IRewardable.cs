using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IRewardable
{
    void AddGold(int gold);
    void AddExp(int exp);
    int AddItem(Item item);
}
