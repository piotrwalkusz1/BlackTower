using UnityEngine;
using System;
using System.Collections;

public class MonsterRespawnLimit : MonsterRespawn, IMonsterRespawnLimit
{
    public int _maxAvailableMonsters;

    protected int _availableMonsters;

    void Awake()
    {
        _availableMonsters = _maxAvailableMonsters;
    }

    new protected void Update()
    {
        if (CanRespawn())
        {
            CreateMonster();         

            SetNextRespawnTime();

            DecreaseAvailableMonster();
        }
    }

    public void IncreaseAvailableMonsters()
    {
        _availableMonsters++;

        if (_availableMonsters == 1)
        {
            SetNextRespawnTime();
        }
    }

    protected void DecreaseAvailableMonster()
    {
        _availableMonsters--;
    }

    protected override bool CanRespawn()
    {
        return DateTime.UtcNow > _nextRespawnTime && IsAvailableMonster();
    }

    protected override void CreateMonster()
    {
        GameObject monster = SceneBuilder.CreateMonster(monsterId, transform.position, transform.eulerAngles.y);

        monster.AddComponent<RespawnConnection>()._respawn = this;
    }

    protected bool IsAvailableMonster()
    {
        return _availableMonsters > 0;
    }
}
