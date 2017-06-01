using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject.Connection.ToServer;

public class Merchant : MonoBehaviour
{
    public int _shopId;

    void OnMouseDown()
    {
        var netOwnPlayer = Client.GetNetOwnPlayer();

        Vector3 offset = netOwnPlayer.transform.position - transform.position;

        if (offset.sqrMagnitude <= NetworkProject.Settings.talkNPCRange * NetworkProject.Settings.talkNPCRange)
        {
            StartTrade();
        }
    }

    public void StartTrade()
    {
        int idNet = GetComponent<NetObject>().IdNet;

        var request = new OpenShopToServer(idNet);

        Client.SendRequestAsMessage(request);
    }
}
