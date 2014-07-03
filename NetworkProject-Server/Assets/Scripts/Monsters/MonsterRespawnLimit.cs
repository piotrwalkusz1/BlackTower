using UnityEngine;
using System;
using System.Collections;

public class MonsterRespawnLimit : MonsterRespawn
{
    public int _availableMonsters;

    new protected void Update()
    {
        if (CanRespawn())
        {
            GameObject monster = CreateMonster();

            monster.AddComponent<RespawnConnection>()._respawn = this;

            SetNextRespawnTime();

            DecreaseAvailableMonster();
        }
    }

    public void IncreaseAvailableMonsters()
    {
        _availableMonsters++;
    }

    protected void DecreaseAvailableMonster()
    {
        _availableMonsters--;
    }

    protected override bool CanRespawn()
    {
        return DateTime.UtcNow > _nextRespawnTime && IsAvailableMonster();
    }

    protected bool IsAvailableMonster()
    {
        return _availableMonsters > 0;
    }
}
