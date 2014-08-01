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

    public void SetItemsInBag(Item[] items)
    {
        _equipment.SetItems(items);
    }

    public void SetSlot(Item item, int idSlot)
    {
        _equipment.UpdateSlot(idSlot, item);

        GUIController.IfActiveEquipmentRefresh();
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

        GUIController.IfActiveEquipmentRefresh();
    }

    public bool IsEmptyBagSlot(int idSlot)
    {
        return GetItemFromBag(idSlot) == null;
    }

    public Item GetSlot(int idSlot)
    {
        return _equipment.GetItem(idSlot);
    }
}
