using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class HotkeysUsableItem : HotkeysObject
{
    public int SlotInBag { get; set; }

    public HotkeysUsableItem(int slotInBag)
    {
        SlotInBag = slotInBag;
    }

    public override Texture2D GetImage()
    {
        try
        {
            int idTexture = Client.GetNetOwnPlayer().GetComponent<OwnPlayerEquipment>().GetSlot(SlotInBag).ItemData.IdTexture;

            return ImageRepository.GetImageByIdImage(idTexture);
        }
        catch
        {
            return null;
        }
    }

    public override void Use()
    {
        try
        {
            GameObject player = Client.GetNetOwnPlayer().gameObject;

            Client.GetNetOwnPlayer().GetComponent<OwnPlayerEquipment>().GetSlot(SlotInBag).ItemData.UseItem(new UseItemInfo(player, SlotInBag));
        }
        catch
        {
        }
    }

    public override string GetDescription()
    {
        try
        {
            return Client.GetNetOwnPlayer().GetComponent<OwnPlayerEquipment>().GetSlot(SlotInBag).ItemData.GetDescription();
        }
        catch
        {
            return null;
        }
    }

    public override bool IsEmpty()
    {
        return Client.GetNetOwnPlayer().GetComponent<OwnPlayerEquipment>().GetSlot(SlotInBag) == null;
    }
}
