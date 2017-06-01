using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;
using UnityEngine;

public class ActionExecutorGiveItem : ActionExecutor
{
    public int _itemId;

    public override void ExecuteAction(NetPlayer player)
    {
        Vector3 offset = transform.position - player.transform.position;

        if (offset.sqrMagnitude > Settings.talkNPCRange * Settings.talkNPCRange)
        {
            MonoBehaviour.print("Za dalego, aby dać item");
            return;
        }

        var equipment = player.GetComponent<PlayerEquipment>();

        if (equipment.IsAnyEmptyBagSlot())
        {
            equipment.AddItemAndSendUpdate(new Item(_itemId));
        }
        else
        {
            Server.SendTextMessage(TextMessage.ItemBagIsFull, player.OwnerAddress);
        }
    }
}
