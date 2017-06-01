using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MonsterMultiRespawnLimit : MonoBehaviour, IMonsterRespawnLimit
{
    public MonsterRespawnMultiManual[] _respawns;  

    public int _maxAvailableMonsters;

    protected int _availableMonsters;

    public float _time;
    protected DateTime _nextRespawnTime;

    protected void Awake()
    {
        _availableMonsters = _maxAvailableMonsters;

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

    protected  bool CanRespawn()
    {
        return DateTime.UtcNow > _nextRespawnTime && IsAvailableMonster();
    }

    protected void SetNextRespawnTime()
    {
        _nextRespawnTime = DateTime.UtcNow.AddSeconds(_time);
    }

    protected void CreateMonster()
    {
        List<MonsterRespawnMultiManual> respawns = new List<MonsterRespawnMultiManual>(_respawns);

        while (respawns.Count > 0)
        {
            int id = UnityEngine.Random.Range(0, respawns.Count);

            if (respawns[id].Respawn())
            {
                DecreaseAvailableMonster();
                break;
            }
            else
            {
                respawns.RemoveAt(id);
            }
        }

    }

    protected bool IsAvailableMonster()
    {
        return _availableMonsters > 0;
    }
}
