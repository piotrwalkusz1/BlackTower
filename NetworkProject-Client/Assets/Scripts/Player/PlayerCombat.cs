using UnityEngine;
using System.Collections;
using NetworkProject;
using NetworkProject.Connection.ToClient;

public class PlayerCombat : Combat
{
    public AudioClip _attackSound;

    public int AttackSpeed { get; set; }
    public int MinDmg { get; set; }
    public int MaxDmg { get; set; }

    protected float TimeBetweenAttacks
    {
        get { return NetworkProject.Settings.GetTimeBetweenAttacks(AttackSpeed); }
    }

    public virtual void Attack()
    {
        GetComponent<PlayerAnimation>().Attack();

        audio.PlayOneShot(_attackSound);
    }

    public override void Attack(AttackToClient attackInfo)
    {
        Attack();
    }
}
