using UnityEngine;
using System.Collections;
using NetworkProject;

[System.CLSCompliant(false)]
public class PlayerStats : Stats
{
    public virtual int HP
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
    public virtual int MaxHP
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
    public virtual int Defense { get; set; }
    public virtual float RegenerationHP { get; set; }
    public virtual float RegenerationMP { get; set; }
    public virtual float AttackSpeed
    {
        get
        {
            return GetComponent<PlayerCombat>().AttackSpeed;
        }
        set
        {
            GetComponent<PlayerCombat>().AttackSpeed = value;
        }
    }
    public virtual float MovementSpeed
    {
        get
        {
            return GetComponent<CharacterMotor>().movement.maxForwardSpeed;
        }
		set
		{
            var motor = GetComponent<CharacterMotor>();
            motor.movement.maxForwardSpeed = value;
            motor.movement.maxSidewaysSpeed = value;
            motor.movement.maxBackwardsSpeed = value;
		}	
    }
    public virtual Breed Breed { get; set; }

    public void Set(PlayerStatsPackage package)
    {
        HP = package._hp;
        MaxHP = package._maxHp;
        Defense = package._defense;
        RegenerationHP = package._hpRegeneration;
        //RegenerationMP
        AttackSpeed = package._attackSpeed;
        MovementSpeed = package._movementSpeed;
        Breed = package._breed;
    }

    public override void Set(IncomingMessage message)
    {
        var package = message.Read<PlayerStatsPackage>();

        Set(package);
    }
}
