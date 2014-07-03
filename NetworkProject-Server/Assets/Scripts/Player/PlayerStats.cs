using UnityEngine;
using System;
using System.Collections;
using NetworkProject;

[System.CLSCompliant(false)]
public class PlayerStats : MonoBehaviour
{
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
            return GetComponent<PlayerCombat>()._attackSpeed;
        }
        set
        {
            GetComponent<PlayerCombat>()._attackSpeed = value;
        }
    }
    public virtual int MinDmg
    {
        get
        {
            return GetComponent<PlayerCombat>()._minDmg;
        }
        set
        {
            GetComponent<PlayerCombat>()._minDmg = value;
        }
    }
    public virtual int MaxDmg
    {
        get
        {
            return GetComponent<PlayerCombat>()._maxDmg;
        }
        set
        {
            GetComponent<PlayerCombat>()._maxDmg = value;
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

    public void CalculateStatsAndSendUpdate()
    {
        CalculateStats();
        SendUpdateToOwner();
        SendUpdateToOtherPlayer();
    }

    public void SendUpdateToOwner()
    {
        var package = GetOwnPlayerStatsPackage();
        var address = GetComponent<NetPlayer>().Address;

        Server.SendMessageUpdateYourAllStats(package, address);
    }

    public void SendUpdateToOtherPlayer()
    {
        GetComponent<NetPlayer>().SendChangeAllStatsMessage();
    }

    public OwnPlayerStatsPackage GetOwnPlayerStatsPackage()
    {
        var package = new OwnPlayerStatsPackage();
        package._attackSpeed = AttackSpeed;
        package._breed = Breed;
        package._defense = Defense;
        package._hp = Hp;
        package._hpRegeneration = RegenerationHP;
        package._maxDmg = MaxDmg;
        package._maxHp = MaxHP;
        package._minDmg = MinDmg;
        package._movementSpeed = MovementSpeed;

        return package;
    }

    public OtherPlayerStatsPackage GetOtherPlayerStatsPackage()
    {
        var package = new OtherPlayerStatsPackage();
        package._attackSpeed = AttackSpeed;
        package._breed = Breed;
        package._defense = Defense;
        package._hp = Hp;
        package._hpRegeneration = RegenerationHP;
        package._maxDmg = MaxDmg;
        package._maxHp = MaxHP;
        package._minDmg = MinDmg;
        package._movementSpeed = MovementSpeed;

        return package;
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

    private void ApplyWeapon(PlayerEquipment equipment)
    {
        if (equipment._weapon == null)
        {
            return;
        }

        WeaponInfo weapon = ItemRepository.GetWeapon(equipment._weapon.IdItem);

        ApplyWeaponStats(weapon);
        ApplyItemBenefits(weapon);
    }

    private void ApplyWeaponStats(WeaponInfo weapon)
    {
        MinDmg = weapon._minDmg;
        MaxDmg = weapon._maxDmg;
        AttackSpeed = weapon._attackSpeed;
    }   

    private void ApplyArmor(PlayerEquipment equipment)
    {
        if (equipment._armor == null)
        {
            return;
        }

        ArmorInfo armor = ItemRepository.GetArmor(equipment._armor.IdItem);

        ApplyArmorStats(armor);
        ApplyItemBenefits(armor);
    }

    private void ApplyArmorStats(ArmorInfo armor)
    {
        Defense += armor._defense;
    }

    private void ApplyItemBenefits(ItemInfo item)
    {
        foreach (ItemBenefit benefit in item.GetBenefits())
        {
            ApplyBenefit(benefit);
        }
    }

    private void ApplyBenefit(ItemBenefit benefit)
    {
        var method = ChooseMethodApplyBenefit(benefit._type);

        method(benefit._value);
    }

    private Action<object> ChooseMethodApplyBenefit(ItemBenefitType type)
    {
        switch (type)
        {
            default:
                throw new System.Exception("Nie ma takiego benefitu : " + type.ToString());
        }
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
