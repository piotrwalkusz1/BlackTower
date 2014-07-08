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
    public virtual int BreedAndGender { get; set; }

    public void Set(IPlayerStats stats)
    {
        HP = stats.HP;
        MaxHP = stats.MaxHP;
        Defense = stats.Defense;
        RegenerationHP = stats.HPRegeneration;
        //RegenerationMP
        AttackSpeed = stats.AttackSpeed;
        MovementSpeed = stats.MovementSpeed;
        Breed = stats.BreedAndGender;
    }
}
