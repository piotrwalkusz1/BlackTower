using UnityEngine;
using System.Collections;

public class MonsterCombat : Combat
{
    public int MinDmg { get; set; }
    public int MaxDmg { get; set; }
    public int AttackSpeed { get; set; }

    public override void Attack(NetworkProject.Connection.ToClient.AttackToClient attackInfo)
    {
        MonsterAnimation animation = GetComponent<MonsterAnimation>();

        animation.Attack();
    }
}