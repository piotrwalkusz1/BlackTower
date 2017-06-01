using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using NetworkProject;
using NetworkProject.Connection;
using NetworkProject.Connection.ToClient;

public class PlayerStats : Stats, IPlayerStats
{
    public virtual int Lvl
    {
        get
        {
            return GetComponent<PlayerExperience>().Lvl;
        }
        set
        {
            GetComponent<PlayerExperience>().Lvl = value;
        }
    }
    public virtual int Exp
    {
        get { return GetComponent<PlayerExperience>().Exp; }
        set { GetComponent<PlayerExperience>().SetExp(value); }
    }
    public virtual int HP
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
    public virtual float HPRegeneration
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
    public virtual int Mana
    { 
        get { return GetComponent<SpellCaster>().Mana; }
        set { GetComponent<SpellCaster>().Mana = value; }
    }
    public virtual int MaxMana
    {
        get { return GetComponent<SpellCaster>().MaxMana; }
        set { GetComponent<SpellCaster>().MaxMana = value; }
    }
    public virtual float ManaRegeneration
    {
        get { return GetComponent<SpellCaster>().ManaRegeneration; }
        set { GetComponent<SpellCaster>().ManaRegeneration = value; }
    }
    public virtual float MovementSpeed { get; set; }
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
    public virtual BreedAndGender BreedAndGender
    {
        get
        {
            return GetComponent<NetPlayer>().BreadAndGender;
        }
        set
        {
            GetComponent<NetPlayer>().BreadAndGender = value;
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
        var request = new UpdateAllStatsToClient(netPlayer.IdNet, this);
        var message = new OutgoingMessage(request);

        Server.Send(message, netPlayer.OwnerAddress);
    }

    public void SendUpdateToOtherPlayer()
    {
        GetComponent<NetPlayer>().SendChangeAllStatsMessage();
    }

    public void Set(RegisterCharacter characterData)
    {
        BreedAndGender = characterData.BreedAndGender;
    }

    public virtual void CalculateStats()
    {
        int hp = HP;
        int mana = Mana;

        StatsToDefault();

        ApplyItems();
        ApplyBuffs();

        if (hp > MaxHP)
        {
            HP = MaxHP;
        }
        else
        {
            HP = hp;
        }

        if (mana > MaxMana)
        {
            Mana = MaxMana;
        }
        else
        {
            Mana = mana;
        }
    }

    private void ApplyItems()
    {
        GetComponent<PlayerEquipment>().ApplyEquipedItemsToStats();
    }

    private void ApplyBuffs()
    {
        GetComponent<PlayerBuff>().ApplyToStats(this);
    }

    private void StatsToDefault()
    {
        Standard.Settings.StatsToDefault(this);
    }
}
