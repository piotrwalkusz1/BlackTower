using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject.Connection;

public class GameObjectRepositoryByFind : IGameObjectRepository
{
    public NetItem[] GetNetItems()
    {
        return GameObject.FindObjectsOfType<NetItem>() as NetItem[];
    }

    public NetPlayer[] GetNetPlayers()
    {
        return GameObject.FindObjectsOfType<NetPlayer>() as NetPlayer[];
    }

    public PlayerRespawn[] GetPlayerRespawns()
    {
        return GameObject.FindObjectsOfType<PlayerRespawn>() as PlayerRespawn[];
    }
}
