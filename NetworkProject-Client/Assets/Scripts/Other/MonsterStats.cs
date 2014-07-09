using UnityEngine;
using System.Collections;
using NetworkProject;

public class MonsterStats : Stats, IMonsterStats
{
    public int HP
    {
        get
        {
            return GetComponent<HP>()._hp;
        }
        set
        {
            GetComponent<HP>()._hp = value;
        }
    }

    public int MaxHP
    {
        get
        {
            return GetComponent<HP>()._maxHp;
        }
        set
        {
            GetComponent<HP>()._maxHp = value;
        }
    }
}
