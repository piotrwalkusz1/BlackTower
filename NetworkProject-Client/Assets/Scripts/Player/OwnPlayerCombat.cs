using UnityEngine;
using System;
using System.Collections;
using InputsSystem;

[System.CLSCompliant(false)]
public class OwnPlayerCombat :PlayerCombat
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

            _newAttackTime = DateTime.UtcNow.AddSeconds(AttackSpeed);
        }
	}

    new public void Attack()
    {
        SendMessage("OnAttack1");

        Vector3 direction = transform.TransformDirection(Vector3.forward);

        Client.SendMessageAttack(direction);
    }

    bool CanAttack()
    {
        return DateTime.UtcNow > _newAttackTime && GetComponent<PlayerEquipement>().Weapon != null;
    }
}
