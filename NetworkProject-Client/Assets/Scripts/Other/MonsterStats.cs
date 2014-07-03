using UnityEngine;
using System.Collections;

[System.CLSCompliant(false)]
public abstract class MonsterStats : Stats
{
    public abstract int Hp { get; set; }

    public abstract int MaxHp { get; set; }

    public abstract float MovingSpeed { get; set; }
}
