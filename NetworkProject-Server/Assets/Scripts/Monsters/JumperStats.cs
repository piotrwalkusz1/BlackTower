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

    public override int MaxHP
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

    public override float MovementSpeed
    {
        get
        {
            return GetComponent<JumperMovement>()._moveSpeed;
        }
        set
        {
            GetComponent<JumperMovement>()._moveSpeed = value;
        }
    }
}
