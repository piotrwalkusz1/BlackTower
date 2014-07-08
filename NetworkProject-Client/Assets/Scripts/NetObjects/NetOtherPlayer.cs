using UnityEngine;
using System.Collections;
using NetworkProject;
using NetworkProject.Connection.ToClient;

[System.CLSCompliant(false)]
public class NetOtherPlayer : NetPlayer
{
    public PlayerMovement Movement { get; set; }
    public OtherPlayerAnimation Animation { get; set; }
    public OtherPlayerCombat Combat { get; set; }

    new protected void Awake()
    {
        base.Awake();

        Movement = GetComponent<PlayerMovement>();
        Animation = GetComponent<OtherPlayerAnimation>();
        Combat = GetComponent<OtherPlayerCombat>();
    }

    public void InitializePlayer(CreateOtherPlayerToClient package)
    {
        IdNet = package.IdNet;
        Name = package.Name;

        var stats = GetComponent<OtherPlayerStats>();
        stats.Set(package.PlayerStats);
    }
}
