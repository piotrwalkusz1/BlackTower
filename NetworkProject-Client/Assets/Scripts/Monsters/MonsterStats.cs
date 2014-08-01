using UnityEngine;
using System.Collections;
using NetworkProject;
using NetworkProject.Connection;

public class MonsterStats : Stats, IMonsterStats
{
    public int HP
    {
        get
        {
            return GetComponent<Health>().HP;
        }
        set
        {
            GetComponent<Health>().HP = value;
        }
    }

    public int MaxHP
    {
        get
        {
            return GetComponent<Health>().MaxHP;
        }
        set
        {
            GetComponent<Health>().MaxHP = value;
        }
    }

    public int MinDmg
    {
        get { return GetComponent<MonsterCombat>().MinDmg; }
        set { GetComponent<MonsterCombat>().MinDmg = value; }
    }

    public int MaxDmg
    {
        get { return GetComponent<MonsterCombat>().MaxDmg; }
        set { GetComponent<MonsterCombat>().MinDmg = value; }
    }

    public int AttackSpeed
    {
        get { return GetComponent<MonsterCombat>().AttackSpeed; }
        set { GetComponent<MonsterCombat>().AttackSpeed = value; }
    }

    public float MovementSpeed
    {
        get { return GetComponent<MonsterMovement>().MovementSpeed; }
        set { GetComponent<MonsterMovement>().MovementSpeed = value; }
    }
}
