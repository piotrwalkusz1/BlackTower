using UnityEngine;
using System.Collections;
using NetworkProject;

public class MonsterStats : Stats, IMonsterStats
{
    public int HP
    {
        get
        {
            return GetComponent<Health>()._hp;
        }
        set
        {
            GetComponent<Health>()._hp = value;
        }
    }

    public int MaxHP
    {
        get
        {
            return GetComponent<Health>()._maxHp;
        }
        set
        {
            GetComponent<Health>()._maxHp = value;
        }
    }
}
