using UnityEngine;
using System.Collections;
using NetworkProject;

[System.CLSCompliant(false)]
public class NetItem : NetObject
{
    public Item Item { get; set; }

    public override void SendMessageAppeared(IConnectionMember address)
    {
        ItemPackage item = new ItemPackage();
        item.IdObject = IdNet;
        item.IdItem = Item.IdItem;
        item.Position = transform.position;

        Server.SendMessageCreateItemObject(item, address);
    }

    public override void SendMessageUpdate(IConnectionMember address)
    {
        //item doesn't need update
    }

    public override void SendMessageDisappeared(IConnectionMember address)
    {
        Server.SendMessageDeleteObject(IdNet, address);
    }

    public bool PlayerIsEnoughCloseToPick(NetPlayer player)
    {
        Vector3 offset = transform.position - player.transform.position;

        return offset.sqrMagnitude <= Settings.pickItemRange * Settings.pickItemRange;
    }
}
