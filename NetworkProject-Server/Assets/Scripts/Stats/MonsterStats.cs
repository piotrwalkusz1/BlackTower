using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NetworkProject;

[System.CLSCompliant(false)]
public class MonsterStats : MonoBehaviour, IMonsterStats
{
    public virtual int HP
    {
        get
        {
            return GetComponent<HealthSystem>().HP;
        }
        set
        {
            GetComponent<HealthSystem>().ChangeHp(value);
        }
    }

    public virtual int MaxHP
    {
        get
        {
            return GetComponent<HealthSystem>().MaxHP;
        }
        set
        {
            GetComponent<HealthSystem>().ChangeHp(value);
        }
    }

    public virtual float MovementSpeed { get; set; }

    public virtual float AttackSpeed
    {
        get
        {
            return GetComponent<MonsterCombat>().AttackSpeed;
        }
        set
        {
            GetComponent<MonsterCombat>().AttackSpeed = value;
        }
    }
}
