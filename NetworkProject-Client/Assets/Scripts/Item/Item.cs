using UnityEngine;
using System;
using System.Collections;
using NetworkProject;

[System.CLSCompliant(false)]
public class Item
{
    public int IdItem { get; set; }

    public Item(int idItem)
    {
        IdItem = idItem;
    }

    public bool CanBeEquipedByPlayer()
    {
        ItemInfo item = ItemBase.GetAnyItem(IdItem)._item;
        NetOwnPlayer player = Client.GetNetOwnPlayer();

        foreach (ItemRequirement requirement in item.GetRequirement())
        {
            Func<NetOwnPlayer, object, bool> func = ChooseMethod(requirement._type);
            if (!func(player, requirement._value))
            {
                return false;
            }
        }

        return true;
    }

    public bool CanBeEquipedByPlayer(ItemEquipableType type)
    {
        ItemInfo item = ItemBase.GetAnyItem(IdItem)._item;
        NetOwnPlayer player = Client.GetNetOwnPlayer();

        if (!IsProperType(item, type))
        {
            return false;
        }

        foreach (ItemRequirement requirement in item.GetRequirement())
        {
            Func<NetOwnPlayer, object, bool> func = ChooseMethod(requirement._type);
            if (!func(player, requirement._value))
            {
                return false;
            }
        }

        return true;
    }

    private bool IsProperType(ItemInfo item, ItemEquipableType type)
    {
        switch (type)
        {
            case ItemEquipableType.RightHand:
                return item is WeaponInfo;
            case ItemEquipableType.LeftHand:
                return item is ShieldInfo;
            case ItemEquipableType.Chest:
                return item is ArmorInfo;
            case ItemEquipableType.Head:
                return item is HelmetInfo;
            case ItemEquipableType.Shoes:
                return item is ShoesInfo;
            case ItemEquipableType.Addition1:
                return item is AdditionInfo;
            case ItemEquipableType.Addition2:
                return item is AdditionInfo;
            default:
                throw new System.Exception("Nie ma takiej części ciała");

        }
    }

    private Func<NetOwnPlayer, object, bool> ChooseMethod(ItemRequirementType type)
    {
        switch (type)
        {
            case ItemRequirementType.Lvl:
                return CheckLvlRequirement;
            default:
                throw new Exception("Nie ma takiego wymagania : " + type.ToString());

        }
    }

    private bool CheckLvlRequirement(NetOwnPlayer player, object lvlRquired)
    {
        return (player.GetComponent<PlayerExperience>().Lvl >= (int)lvlRquired);
    }
}
