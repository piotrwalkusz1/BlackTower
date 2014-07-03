using UnityEngine;
using System.Collections;

[System.CLSCompliant(false)]
public class JumperStats : MonsterStats
{
    public override int HP
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

    public override int MaxHp
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

    public override float MovingSpeed
    {
        get
        {
            return GetComponent<JumperMovementSystem>()._moveSpeed;
        }
        set
        {
            GetComponent<JumperMovementSystem>()._moveSpeed = value;
        }
    }
}
