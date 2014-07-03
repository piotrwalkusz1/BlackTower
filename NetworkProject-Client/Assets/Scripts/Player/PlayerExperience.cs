using UnityEngine;
using System.Collections;
using NetworkProject;

[System.CLSCompliant(false)]
public class PlayerExperience : Experience
{
    public override int MaxExp
    {
        get
        {
            return Settings.MaxExpInLvl(Lvl);
        }
    }

    public void Set(int lvl, int exp)
    {
        Lvl = lvl;
        Exp = exp;
    }
}
