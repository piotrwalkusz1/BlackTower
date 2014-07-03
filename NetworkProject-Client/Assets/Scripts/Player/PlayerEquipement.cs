using UnityEngine;
using System.Collections;
using NetworkProject;

[System.CLSCompliant(false)]
public class PlayerEquipement : Equipment, IEquippedItems
{
    public Item Weapon { get; set; }
    public Item AdditionalWeapon { get; set; }
    public Item Armor { get; set; }
    public Item Helmet { get; set; }
    public Item Shoes { get; set; }
    public Item Addition1 { get; set; }
    public Item Addition2 { get; set; }

    public void Set(EquipedItemsPackage package)
    {
        Weapon = package._weapon == -1 ? null : new Item(package._weapon);
        AdditionalWeapon = package._additionalWeapon == -1 ? null : new Item(package._additionalWeapon);
        Armor = package._armor == -1 ? null : new Item(package._armor);
        Helmet = package._helmet == -1 ? null : new Item(package._helmet);
        Shoes = package._shoes == -1 ? null : new Item(package._shoes);
        Addition1 = package._addition1 == -1 ? null : new Item(package._addition1);
        Addition2 = package._addition2 == -1 ? null : new Item(package._addition2);
    }

    public void Equipe(Item item, ItemEquipableType place)
    {
        switch (place)
        {
            case ItemEquipableType.RightHand:
                Weapon = item;
                break;
            case ItemEquipableType.LeftHand:
                AdditionalWeapon = item;
                break;
            case ItemEquipableType.Chest:
                Armor = item;
                break;
            case ItemEquipableType.Head:
                Helmet = item;
                break;
            case ItemEquipableType.Shoes:
                Shoes = item;
                break;
            case ItemEquipableType.Addition1:
                Addition1 = item;
                break;
            case ItemEquipableType.Addition2:
                Addition2 = item;
                break;
            default:
                throw new System.Exception("Nie ma takiej część ciała.");
        }
    }

    public Item GetEquipedItem(ItemEquipableType place)
    {
        switch (place)
        {
            case ItemEquipableType.RightHand:
                return Weapon;
            case ItemEquipableType.LeftHand:
                return AdditionalWeapon;
            case ItemEquipableType.Chest:
                return Armor;
            case ItemEquipableType.Head:
                return Helmet;
            case ItemEquipableType.Shoes:
                return Shoes;
            case ItemEquipableType.Addition1:
                return Addition1;
            case ItemEquipableType.Addition2:
                return Addition2;
            default:
                throw new System.Exception("Nie ma takiej część ciała.");
        }
    }

    public bool IsEmpty(ItemEquipableType place)
    {
        Item item = GetEquipedItem(place);

        return item == null;
    }
}
