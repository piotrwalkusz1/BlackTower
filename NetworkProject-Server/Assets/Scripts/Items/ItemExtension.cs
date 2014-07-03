using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;
using NetworkProject.BodyParts;

public static class ItemExtension
{
    public static bool CanBeEquipedByPlayer(this Item item, NetPlayer player)
    {
        ItemInfo itemInfo = ItemRepository.GetAnyItem(item.IdItem);

        foreach (ItemRequirement requirement in itemInfo.GetRequirement())
        {
            Func<NetPlayer, object, bool> func = ChooseMethodCheckRequirement(requirement._type);
            if (!func(player, requirement._value))
            {
                return false;
            }
        }

        return true;
    }

    public static bool CanBeEquipedByPlayerOnThisBodyPart(this Item item, NetPlayer player, BodyPartType bodyPart)
    {
        ItemInfo itemInfo = ItemRepository.GetAnyItem(item.IdItem);

        if (!IsProperType(itemInfo, bodyPart))
        {
            return false;
        }

        foreach (ItemRequirement requirement in itemInfo.GetRequirement())
        {
            Func<NetPlayer, object, bool> func = ChooseMethodCheckRequirement(requirement._type);
            if (!func(player, requirement._value))
            {
                return false;
            }
        }

        return true;
    }

    private static bool IsProperType(ItemInfo itemInfo, BodyPartType bodyPartType)
    {
        BodyPart bodyPart = IoC.GetBodyPart(bodyPartType);

        return bodyPart.CanEquipeItemOnThisBodyPart(itemInfo);
    }

    private static Func<NetPlayer, object, bool> ChooseMethodCheckRequirement(ItemRequirementType type)
    {
        switch (type)
        {
            case ItemRequirementType.Lvl:
                return CheckLvlRequirement;
            default:
                throw new Exception("Nie ma takiego wymagania : " + type.ToString());

        }
    }

    private static bool CheckLvlRequirement(NetPlayer player, object lvlRquired)
    {
        return (player.GetComponent<PlayerExperience>().Lvl >= (int)lvlRquired);
    }
}
