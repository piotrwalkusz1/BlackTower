using UnityEngine;
using System.Collections;
using NetworkProject.Connection.ToClient;

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
            return NetworkProject.Settings.GetMaxExpInLvl(Lvl);
        }
    }

    public void Set(int lvl, int exp)
    {
        Lvl = lvl;
        Exp = exp;
    }

    public void SendUpdateExpToOwner()
    {
        NetPlayer netPlayer = GetComponent<NetPlayer>();

        var request = new UpdateExperience(netPlayer.IdNet, Exp);

        Server.SendRequestAsMessage(request, netPlayer.OwnerAddress);
    }
}
