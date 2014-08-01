using UnityEngine;
using System;
using System.Collections;
using InputsSystem;
using NetworkProject.Connection.ToServer;

public class OwnPlayerCombat : PlayerCombat
{
    private DateTime _newAttackTime = DateTime.UtcNow;

	void Start()
    {
	}
	
	void Update()
    {
        if(CanAttack() && Inputs.IsPressedButton("Attack"))
        {
            Attack();

            _newAttackTime = DateTime.UtcNow.AddSeconds(TimeBetweenAttacks);
        }
	}

    public override void Attack()
    {
        base.Attack();

        Vector3 direction = transform.TransformDirection(Vector3.forward);

        var request = new AttackToServer(direction);

        Client.SendRequestAsMessage(request);
    }

    private bool CanAttack()
    {
        return DateTime.UtcNow > _newAttackTime && GetComponent<PlayerEquipment>().IsEquipedWeapon();
    }
}
