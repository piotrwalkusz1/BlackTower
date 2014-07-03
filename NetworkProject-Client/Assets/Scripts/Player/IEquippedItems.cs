using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;

public interface IEquippedItems
{
    Item Weapon { get; set; }
    Item AdditionalWeapon { get; set; }
    Item Armor { get; set; }
    Item Helmet { get; set; }
    Item Shoes { get; set; }
    Item Addition1 { get; set; }
    Item Addition2 { get; set; }

    void Set(EquipedItemsPackage package);

    void Equipe(Item item, ItemEquipableType place);

    Item GetEquipedItem(ItemEquipableType place);

    bool IsEmpty(ItemEquipableType place);
}

