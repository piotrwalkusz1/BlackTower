using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject.Connection;

public static class GameObjectRepository
{
    private static IGameObjectRepository _repository;

    static GameObjectRepository()
    {
        _repository = Standard.IoC.GetGameObjectRepository();
    }

    public static void Set(IGameObjectRepository repository)
    {
        _repository = repository;
    }

    public static NetItem FindNetItemByIdNet(int idNet)
    {
        NetItem[] items = _repository.GetNetItems();

        return items.First(x => x.IdNet == idNet);
    }

    public static PlayerRespawn FindNearestPlayerRespawnOnMap(int map, Vector3 position)
    {
        PlayerRespawn[] respawns = _repository.GetPlayerRespawns();

        var respawnsAndSqrDistances = from respawn in respawns
                                      where respawn.GetMap() == map
                                      select new
                                      {
                                          Obj = respawn,
                                          SqrDistance = (position - respawn.transform.position).sqrMagnitude
                                      };

        PlayerRespawn nearestRespawn = null;
        float smallestSqrDistance = Mathf.Infinity;

        foreach (var respawn in respawnsAndSqrDistances)
        {
            if (respawn.SqrDistance < smallestSqrDistance)
            {
                smallestSqrDistance = respawn.SqrDistance;
                nearestRespawn = respawn.Obj;
            }
        }

        return nearestRespawn;
    }
}
