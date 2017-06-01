using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NetworkProject;
using NetworkProject.Connection;

public class MonsterStats : Stats, IMonsterStats
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
            GetComponent<HealthSystem>().ChangeMaxHP(value);
        }
    }

    public virtual float[] MovementSpeed
    {
        get
        {
            return GetComponent<MonsterMovement>().MovementSpeedByType;
        }
        set
        {
            GetComponent<MonsterMovement>().MovementSpeedByType = value;
        }
    }

    public virtual int MinDmg
    {
        get { return GetComponent<MonsterCombat>().MinDmg; }
        set { GetComponent<MonsterCombat>().MinDmg = value; }
    }

    public virtual int MaxDmg
    {
        get { return GetComponent<MonsterCombat>().MaxDmg; }
        set { GetComponent<MonsterCombat>().MaxDmg = value; }
    }

    public virtual int AttackSpeed
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
