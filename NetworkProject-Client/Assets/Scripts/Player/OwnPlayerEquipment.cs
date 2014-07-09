using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Items;

public class OwnPlayerEquipment : PlayerEquipment, IEquipment
{
    private ItemBag _equipment;

    public OwnPlayerEquipment()
    {
        _equipment = new ItemBag();
    }

    public void SetItemInBag(Item item, int idSlot)
    {
        _equipment.UpdateSlot(idSlot, item);

        GUIController.IsActiveEquipmentRefresh();
    }

    public Item GetItemFromBag(int idSlot)
    {
        return _equipment.GetItem(idSlot);
    }

    public Item[] GetItemsFromBag()
    {
        return _equipment.GetItems();
    }

    public override void Equip(Item item, int bodyPart)
    {
        base.Equip(item, bodyPart);

        GUIController.IsActiveEquipmentRefresh();
    }

    public bool IsEmptyBagSlot(int idSlot)
    {
        return GetItemFromBag(idSlot) == null;
    }

    public void SetSlot(Item item, int idSlot)
    {
        _equipment.UpdateSlot(idSlot, item);
    }

    public Item GetSlot(int idSlot)
    {
        return _equipment.GetItem(idSlot);
    }
}
