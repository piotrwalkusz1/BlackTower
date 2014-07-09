using UnityEngine;
using System.Collections;
using NetworkProject;

[System.CLSCompliant(false)]
public class PlayerStats : Stats, IPlayerStats
{
    public int Lvl
    {
        get
        {
            return GetComponent<Experience>().Lvl;
        }
    }
    public int MinDmg
    {
        get { return GetComponent<PlayerCombat>().MinDmg; }
        set { GetComponent<PlayerCombat>().MinDmg = value; }
    }
    public int MaxDmg
    {
        get { return GetComponent<PlayerCombat>().MaxDmg; }
        set { GetComponent<PlayerCombat>().MaxDmg = value; }
    }  
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
    public virtual BreedAndGender BreedAndGender { get; set; }
    public float HPRegeneration
    {
        get
        {
            throw new System.NotImplementedException();
        }
        set
        {
            throw new System.NotImplementedException();
        }
    }
}
