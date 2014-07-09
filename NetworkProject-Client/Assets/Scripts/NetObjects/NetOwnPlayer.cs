using UnityEngine;
using System.Collections;
using NetworkProject;
using NetworkProject.Connection.ToClient;
using NetworkProject.Connection.ToServer;

public class NetOwnPlayer : NetPlayer
{
	protected void Update()
    {
		if (IsMovement())
        {
            var request = new PlayerMoveToServer(transform.position);

            Client.SendRequestAsMessage(request);
		}

        if (IsRotation())
        {
            var request = new PlayerRotationToServer(transform.eulerAngles.y);

            Client.SendRequestAsMessage(request);
        }
	}

    public void InitializePlayer(CreateOwnPlayerToClient package)
    {
        IdNet = package.IdNet;
        Name = package.Name;

        var stats = GetComponent<OwnPlayerStats>();
        stats.Set(package.PlayerStats);
    }
}
