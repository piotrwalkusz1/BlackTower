using UnityEngine;
using System.Collections;
using NetworkProject;
using NetworkProject.Connection;

public abstract class PlayerStats : Stats, IPlayerStats
{
    public virtual BreedAndGender BreedAndGender
    {
        get { return GetComponent<NetPlayer>().BreedAndGender; }
        set { GetComponent<NetPlayer>().BreedAndGender = value; }
    }
    public virtual int Lvl
    {
        get
        {
            return GetComponent<Experience>().Lvl;
        }
        set
        {
            GetComponent<Experience>().Lvl = value;
        }
    }
    public virtual int MinDmg
    {
        get { return GetComponent<PlayerCombat>().MinDmg; }
        set { GetComponent<PlayerCombat>().MinDmg = value; }
    }
    public virtual int MaxDmg
    {
        get { return GetComponent<PlayerCombat>().MaxDmg; }
        set { GetComponent<PlayerCombat>().MaxDmg = value; }
    }  
    public virtual int HP
    {
        get
        {
            return GetComponent<Health>().HP;
        }
        set
        {
            GetComponent<Health>().HP = value;
        }
    }
    public virtual int MaxHP
    {
        get
        {
            return GetComponent<Health>().MaxHP;
        }
        set
        {
            GetComponent<Health>().MaxHP = value;
        }
    }
    public virtual int Defense { get; set; }
    public virtual int AttackSpeed
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
    public virtual float HPRegeneration
    {
        get { return GetComponent<Health>().HPRegeneration; }
        set { GetComponent<Health>().HPRegeneration = value; }
    }
}
