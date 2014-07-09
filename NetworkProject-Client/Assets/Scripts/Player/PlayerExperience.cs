using UnityEngine;
using System.Collections;
using NetworkProject;

public class PlayerExperience : Experience
{
    public override int MaxExp
    {
        get
        {
            return Settings.GetMaxExpInLvl(Lvl);
        }
    }

    public void Set(int lvl, int exp)
    {
        Lvl = lvl;
        Exp = exp;
    }
}
