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

    public void SetItem(Item item, int idSlot)
    {
        _equipment.UpdateSlot(idSlot, item);

        GUIController.IsActiveEquipmentRefresh();
    }

    public Item GetItem(int idSlot)
    {
        return _equipment.GetItem(idSlot);
    }

    public override void Equipe(Item item, int bodyPart)
    {
        base.Equipe(item, bodyPart);

        GUIController.IsActiveEquipmentRefresh();
    }
}
