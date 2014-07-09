using UnityEngine;
using System.Collections;
using NetworkProject.Connection.ToClient;

public class NetObject : MonoBehaviour
{
	public int IdNet { get; set; }

    public void Respawn(RespawnToClient respawnInfo)
    {
        transform.position = respawnInfo.Position;
    }
}
