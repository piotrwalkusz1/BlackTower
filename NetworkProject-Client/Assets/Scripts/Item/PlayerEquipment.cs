using UnityEngine;
using System.Collections;
using NetworkProject;
using NetworkProject.Items;
using NetworkProject.BodyParts;

public class PlayerEquipment : MonoBehaviour, IEquiper
{
    private PlayerEquipedItems _equipedItems;

    public void SetEquipedItems(PlayerEquipedItemsPackage package)
    {
        _equipedItems = new PlayerEquipedItems(package);
    }

    public virtual void Equip(Item item, int bodyPartSlot)
    {
        _equipedItems.EquipeItem(item, bodyPartSlot);

        GetComponent<PlayerGeneratorModel>().UpdateEquipedItem(_equipedItems.GetBodyPart(bodyPartSlot), bodyPartSlot);
    }

    public BodyPart GetBodyPart(int slot)
    {
        return _equipedItems.GetBodyPart(slot);
    }

    public BodyPart[] GetBodyParts()
    {
        return _equipedItems.GetBodyParts();
    }

    public Item GetEquipedItem(int bodyPart)
    {
        return _equipedItems.GetEquipedItem(bodyPart);
    }

    public bool IsEmptyEquipedSlot(int bodyPart)
    {
        Item item = GetEquipedItem(bodyPart);

        return item == null;
    }

    public bool IsEquipedWeapon()
    {
        return _equipedItems.IsEquipedWeapon();
    }
}
