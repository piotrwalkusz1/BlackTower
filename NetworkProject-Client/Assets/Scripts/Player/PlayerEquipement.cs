using UnityEngine;
using System.Collections;
using NetworkProject;
using NetworkProject.Items;

[System.CLSCompliant(false)]
public class PlayerEquipement
{
    private PlayerEquipedItems _equipedItems;

    public PlayerEquipement()
    {
        _equipedItems = new PlayerEquipedItems();
    }

    public void Set(PlayerEquipedItems package)
    {
        _equipedItems = package;
    }

    public void Equipe(Item item, int bodyPart)
    {
        _equipedItems.EquipeItem(item, bodyPart);
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
}
