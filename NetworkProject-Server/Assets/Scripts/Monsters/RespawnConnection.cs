using UnityEngine;
using System.Collections;

public class RespawnConnection : MonoBehaviour
{
    public IMonsterRespawnLimit _respawn;

    protected void OnDead()
    {
        _respawn.IncreaseAvailableMonsters();
    }
}
