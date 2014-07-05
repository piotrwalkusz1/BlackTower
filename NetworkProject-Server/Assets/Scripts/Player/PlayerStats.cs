using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using NetworkProject;
using NetworkProject.Connection;
using NetworkProject.Connection.ToClient;

[System.CLSCompliant(false)]
public class PlayerStats : MonoBehaviour, IStats
{
    public virtual int Lvl
    {
        get
        {
            return GetComponent<PlayerExperience>().Lvl;
        }
    }   
    public virtual int Hp
    {
        get
        {
            return GetComponent<HealthSystem>().HP;
        }
        set
        {
            GetComponent<HealthSystem>().ChangeHp(value);
        }
    }
    public virtual int MaxHP
    {
        get
        {
            return GetComponent<HealthSystem>().MaxHP;
        }
        set
        {
            GetComponent<HealthSystem>().ChangeMaxHP(value);
        }
    }
    public virtual float RegenerationHP
    {
        get
        {
            return GetComponent<HealthSystem>().HPRegenerationPerSecond;
        }
        set
        {
            GetComponent<HealthSystem>().HPRegenerationPerSecond = value;
        }
    }
    public virtual float MovementSpeed { get; set; }
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
    public virtual int Defense
    {
        get
        {
            return GetComponent<PlayerHealthSystem>().Defense;
        }
        set
        {
            GetComponent<PlayerHealthSystem>().Defense = value;
        }
    }
    public virtual Breed Breed { get; set; }
    public virtual List<int> Damages
    {
        get
        {
            return new List<int>() { MinDmg, MaxDmg };
        }
    }
    public virtual int MinDmg
    {
        get
        {
            return GetComponent<PlayerCombat>().MinDmg;
        }
        set
        {
            GetComponent<PlayerCombat>().MinDmg = value;
        }
    }
    public virtual int MaxDmg
    {
        get
        {
            return GetComponent<PlayerCombat>().MaxDmg;
        }
        set
        {
            GetComponent<PlayerCombat>().MaxDmg = value;
        }
    }

    public void CalculateStatsAndSendUpdate()
    {
        CalculateStats();
        SendUpdateToOwner();
        SendUpdateToOtherPlayer();
    }

    public void SendUpdateToOwner()
    {
        var netPlayer = GetComponent<NetPlayer>();
        var request = new UpdateAllStats(netPlayer.IdNet, this);
        var message = new OutgoingMessage(request);

        Server.Send(message, netPlayer.OwnerAddress);
    }

    public void SendUpdateToOtherPlayer()
    {
        GetComponent<NetPlayer>().SendChangeAllStatsMessage();
    }

    public void Set(RegisterCharacter characterData)
    {
        Breed = characterData.Breed;
    }

    public virtual void CalculateStats()
    {
        int hp = Hp;

        StatsToDefault();

        ApplyItems();

        Hp = hp;
    }

    private void ApplyItems()
    {
        PlayerEquipment eq = GetComponent<PlayerEquipment>();

        ApplyWeapon(eq);
        ApplyArmor(eq);
    }

    private void StatsToDefault()
    {
        MaxHP = Settings.basicPlayerMaxExp;
        MovementSpeed = Settings.basicPlayerMovementSpeed;
        AttackSpeed = 0f;
        MinDmg = 0;
        MaxDmg = 0;
        Defense = 0;
    }
}
