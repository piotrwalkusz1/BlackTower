using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public interface IGameObjectRepository
{
    NetPlayer[] GetNetPlayers();
    NetObject[] GetNetObjects();
    void Delete(GameObject objectToDelete);
}
