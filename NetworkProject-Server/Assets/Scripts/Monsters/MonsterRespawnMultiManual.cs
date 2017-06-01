using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MonsterRespawnMultiManual : MonoBehaviour, IMonsterRespawnLimit
{
    public int _monsterId;
    public int _availableMonsters;
    public MonsterMultiRespawnLimit _respawnParent;

    public bool Respawn()
    {
        if (_availableMonsters > 0)
        {
            CreateMonster();

            _availableMonsters--;

            return true;
        }
        else
        {
            return false;
        }
    }

    protected void CreateMonster()
    {
        GameObject monster = SceneBuilder.CreateMonster(_monsterId, transform.position, transform.eulerAngles.y);

        monster.AddComponent<RespawnConnection>()._respawn = this;
    }

    public void IncreaseAvailableMonsters()
    {
        _availableMonsters++;

        _respawnParent.IncreaseAvailableMonsters();
    }
}
