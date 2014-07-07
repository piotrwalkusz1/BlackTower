using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;
using NetworkProject.BodyParts;
using NetworkProject.Items;

public static class ItemExtension
{
    public static bool CanBeEquipedByPlayer(this Item item, NetPlayer player)
    {
        EquipableItemData itemData = (EquipableItemData)ItemRepository.GetItemByIdItem(item.IdItem);

        return itemData.CanEquipe(player.GetComponent<PlayerStats>());
    }

    public static bool CanBeEquipedByPlayerOnThisBodyPart(this Item item, NetPlayer player, int bodyPartSlot)
    {
        EquipableItemData itemData = (EquipableItemData)ItemRepository.GetItemByIdItem(item.IdItem);

        BodyPart bodyPart = IoC.GetBodyPart(bodyPartSlot);

        if(!bodyPart.CanEquipeItemOnThisBodyPart(itemData))
        {
            return false;
        }

        return itemData.CanEquipe(player.GetComponent<PlayerStats>());
    }
}
