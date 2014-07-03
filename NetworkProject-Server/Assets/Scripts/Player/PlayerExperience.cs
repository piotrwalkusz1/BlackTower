using UnityEngine;
using System.Collections;

[System.CLSCompliant(false)]
public class PlayerExperience : Experience
{
    public PlayerExperience(int lvl, int exp)
    {
        Lvl = lvl;
        Exp = exp;
    }

    public override int MaxExp
    {
        get
        {
            return NetworkProject.Settings.MaxExpInLvl(Lvl);
        }
    }

    public void Set(int lvl, int exp)
    {
        Lvl = lvl;
        Exp = exp;
    }

    public void SendUpdate()
    {
        Server.SendMessageUpdateYourExperience(Lvl, Exp, GetComponent<NetPlayer>().Address);
    }
}
