using UnityEngine;
using System.Collections;

[System.CLSCompliant(false)]
public class PlayerRespawn : MonoBehaviour
{
    public void Respawn(NetPlayer player)
    {
        SceneBuilder.RespawnPlayer(player, this);
    }

    public int GetMap()
    {
        return Standard.Settings.GetMap(transform.position);
    }
}
