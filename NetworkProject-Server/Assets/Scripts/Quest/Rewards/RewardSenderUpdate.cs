using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject.Connection;
using NetworkProject.Connection.ToClient;

public class RewardSenderUpdate
{
    public bool IsGold;
    public bool IsStats;
    public List<int> ItemBagChangedSlots { get; private set; }

    public RewardSenderUpdate()
    {
        ItemBagChangedSlots = new List<int>();
    }

    public void SendUpdate(GameObject player, IConnectionMember address)
    {
        var equipment = player.GetComponent<PlayerEquipment>();
        var netObject = player.GetComponent<NetObject>();

        if (IsGold)
        {
            equipment.SendUpdateGold(address);
        }

        if (IsStats)
        {          
            var stats = player.GetComponent<PlayerStats>();

            stats.CalculateStats();

            var request = new UpdateAllStatsToClient(netObject.IdNet, stats);

            Server.SendRequestAsMessage(request, address);
        }

        foreach (int changeSlot in ItemBagChangedSlots)
        {
            equipment.SendUpdateBagSlot(changeSlot, address);
        }
    }
}
