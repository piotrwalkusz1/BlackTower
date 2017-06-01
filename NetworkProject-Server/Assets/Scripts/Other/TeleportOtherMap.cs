using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject.Connection.ToClient;

public class TeleportOtherMap : MonoBehaviour
{
    public int _teleportTargetId;

    private Transform _target;

    void Start()
    {
        _target = GameObject.FindObjectsOfType<TeleportTarget>().First(x => x._id == _teleportTargetId).transform;
    }

    void OnTriggerEnter(Collider hit)
    {
        NetPlayer netPlayer = hit.gameObject.GetComponent<NetPlayer>();

        if (netPlayer != null)
        {
            int newMap = Standard.Settings.GetMap(_target.position);

            var requestChangeMap = new GoIntoWorldToClient(newMap);

            Server.SendRequestAsMessage(requestChangeMap, netPlayer.OwnerAddress);

            netPlayer.transform.position = _target.position;

            netPlayer.GetComponent<PlayerMovement>().SendUpdatePositionToOwnerAndWaitForResponse();

            netPlayer.GetComponent<Vision>().UpdateMap(newMap);

            netPlayer.SendCreateMessageToOwner();
        }
    }
}
