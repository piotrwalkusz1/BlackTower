using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject;
using NetworkProject.Connection.ToServer;

public class MonsterCombat : Combat
{
    public int MinDmg { get; set; }
    public int MaxDmg { get; set; }
    public int AttackSpeed { get; set; }

    public float AttackRange = 2f;

    public DateTime NextAttackTime { get; private set; }

    public MonsterCombat()
    {
        NextAttackTime = DateTime.UtcNow;
    }

    public void Attack(HealthSystem target)
    {
        int dmg = UnityEngine.Random.Range(MinDmg, MaxDmg);

        var attacker = new Attacker(gameObject);

        var attackInfo = new AttackInfo(attacker, dmg, DamageType.Physical);

        target.AttackWithoutSendUpdate(attackInfo);
        target.SendUpdateHP();

        NextAttackTime = DateTime.UtcNow.AddSeconds(Settings.GetTimeBetweenAttacks(AttackSpeed));

        var netObjectTarget = target.GetComponent<NetObject>();

        if(netObjectTarget != null)
        {
            GetComponent<NetMonster>().SendAttackMessage(netObjectTarget.IdNet);
        }
    }

    public bool IsEnoughTimeToAttack()
    {
        return DateTime.UtcNow > NextAttackTime;
    }

    public bool IsEnoughDistanceToAttack(Vector3 targetPosition)
    {
        var offset = transform.position - targetPosition;

        return AttackRange * AttackRange > offset.sqrMagnitude;
    }
}
