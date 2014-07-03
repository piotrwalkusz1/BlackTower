using UnityEngine;
using System.Collections;
using NetworkProject;

[System.CLSCompliant(false)]
public class OwnPlayerStats : PlayerStats
{
    public override void Set(IncomingMessage message)
    {
        var package = message.Read<OwnPlayerStatsPackage>();

        Set(package);
    }
}
