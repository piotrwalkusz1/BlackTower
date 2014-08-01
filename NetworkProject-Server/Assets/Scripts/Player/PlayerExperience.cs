using UnityEngine;
using System.Collections;
using NetworkProject.Connection.ToClient;

public class PlayerExperience : Experience
{
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

        var request = new UpdateExpToClient(netPlayer.IdNet, Exp);

        Server.SendRequestAsMessage(request, netPlayer.OwnerAddress);
    }
}
