using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject.Connection;

public interface IGameObjectRepository
{
    NetItem[] GetNetItems();

    NetPlayer[] GetNetPlayers();

    PlayerRespawn[] GetPlayerRespawns();

    NetObject[] GetNetObjects();
}
