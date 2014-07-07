using UnityEngine;
using System;
using System.Collections;
using NetworkProject;

[System.CLSCompliant(false)]
public class MonsterRespawn : MonoBehaviour
{
    public int monsterId;
    public float _time;
    protected DateTime _nextRespawnTime;

    protected void Start()
    {
        _nextRespawnTime = DateTime.UtcNow;
    }

    protected void Update()
    {
        if (CanRespawn())
        {
            CreateMonster();

            SetNextRespawnTime();
        }
    }

    protected virtual bool CanRespawn()
    {
        return DateTime.UtcNow > _nextRespawnTime;
    }

    protected GameObject CreateMonster()
    {
        return SceneBuilder.CreateMonster(monsterId, transform.position, transform.eulerAngles.y);
    }

    protected void SetNextRespawnTime()
    {
        _nextRespawnTime = DateTime.UtcNow.AddSeconds(_time);
    }
}
