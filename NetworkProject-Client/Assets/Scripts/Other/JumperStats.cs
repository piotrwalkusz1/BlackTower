using UnityEngine;
using System.Collections;
using NetworkProject;

[System.CLSCompliant(false)]
public class JumperStats : MonsterStats
{
    public override int Hp
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

    public override int MaxHp
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

    public override float MovingSpeed
    {
        get
        {
            return GetComponent<JumperMovement>().Speed;
        }
        set
        {
            GetComponent<JumperMovement>().Speed = value;
        }
    }

    public override void Set(IncomingMessage message)
    {
        var package = message.Read<MonsterStatsPackage>();
        Hp = package._hp;
        MaxHp = package._maxHp;
        MovingSpeed = package._movementSpeed;
    }
}
