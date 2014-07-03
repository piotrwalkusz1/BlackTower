using UnityEngine;
using System.Collections;
using NetworkProject;

[System.CLSCompliant(false)]
public class MonsterStats : MonoBehaviour
{
    public virtual int HP { get; set; }

    public virtual int MaxHp { get; set; }

    public virtual float MovingSpeed { get; set; }

    public MonsterStatsPackage GetMonsterStatsPackage()
    {
        var package = new MonsterStatsPackage();
        package._hp = HP;
        package._maxHp = MaxHp;
        package._movementSpeed = MovingSpeed;

        return package;
    }
}
