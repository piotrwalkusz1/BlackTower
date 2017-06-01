using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GameObjectRepositoryByFind : IGameObjectRepository
{
    public NetPlayer[] GetNetPlayers()
    {
        return GameObject.FindObjectsOfType(typeof(NetPlayer)) as NetPlayer[];
    }

    public NetObject[] GetNetObjects()
    {
        return GameObject.FindObjectsOfType(typeof(NetObject)) as NetObject[];
    }

    public void Delete(GameObject objectToDelete)
    {
        // we have not delete object from repository
    }
}
