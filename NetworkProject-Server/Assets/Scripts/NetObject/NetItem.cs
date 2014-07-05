using UnityEngine;
using System.Collections;
using NetworkProject;
using NetworkProject.Items;
using NetworkProject.Connection;
using NetworkProject.Connection.ToClient;

[System.CLSCompliant(false)]
public class NetItem : NetObject
{
    public Item Item { get; set; }

    public override void SendMessageAppeared(IConnectionMember address)
    {
        var request = new CreateItem(IdNet, transform.position, transform.eulerAngles.y, Item.IdItem);
        var message = new OutgoingMessage(request);


        Server.Send(message, address);
    }

    public override void SendMessageUpdate(IConnectionMember address)
    {
        //item doesn't need update
    }

    public bool PlayerIsEnoughCloseToPick(NetPlayer player)
    {
        Vector3 offset = transform.position - player.transform.position;

        return offset.sqrMagnitude <= Settings.pickItemRange * Settings.pickItemRange;
    }

    public void TryPickByPlayer(NetPlayer player)
    {
        PlayerEquipment eq = player.GetComponent<PlayerEquipment>();

        if (eq.IsFreePlace() && PlayerIsEnoughCloseToPick(player))
        {
            eq.AddItem(Item);

            Destroy(gameObject);
        }
    }
}
