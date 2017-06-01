using UnityEngine;
using System.Collections;
using NetworkProject;
using NetworkProject.Connection.ToClient;

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

    protected void LateUpdate()
    {
        CheckChangePositionAndRotation();
    }

    public void InitializePlayer(CreateOtherPlayerToClient package)
    {
        IdNet = package.IdNet;
        IsModelVisible = package.IsModelVisible;
        Name = package.Name;

        var stats = GetComponent<OtherPlayerStats>();
        package.PlayerStats.CopyToStats(stats);

        var equipment = GetComponent<OtherPlayerEquipment>();
        equipment.SetEquipedItems(package.EquipedItems);
    }
}
