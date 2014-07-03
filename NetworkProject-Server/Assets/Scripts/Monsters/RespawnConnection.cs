using UnityEngine;
using System.Collections;

public class RespawnConnection : MonoBehaviour
{
    public MonsterRespawnLimit _respawn;

    protected void OnDead()
    {
        _respawn.IncreaseAvailableMonsters();
    }
}
