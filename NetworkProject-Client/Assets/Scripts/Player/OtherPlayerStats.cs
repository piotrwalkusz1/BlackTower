using UnityEngine;
using System.Collections;
using NetworkProject;

[System.CLSCompliant(false)]
public class OtherPlayerStats : PlayerStats
{
    public override float MovementSpeed
    {
        get
        {
            return GetComponent<PlayerMovement>().Speed;
        }
        set
        {
            GetComponent<PlayerMovement>().Speed = value;
        }
    }

    public override void Set(IncomingMessage message)
    {
        var package = message.Read<OtherPlayerStatsPackage>();

        Set(package);
    }
}
