using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NetworkProject;

[System.CLSCompliant(false)]
public class ItemData
{
    public ItemInfo _item;

    public int IdItem
    {
        get
        {
            return _item._idItem;
        }
        set
        {
            _item._idItem = value;
        }
    }
    public string _name;
    public int _idImage;
    public int _idPrefabOnScene;
    public int _idPrefabOnPlayer;

    public ItemData()
    {

    }

    public ItemData(ItemInfo item)
    {
        _item = item;
    }

    public ItemData(ItemInfo item, string name)
    {
        _item = item;
        _name = name;
    }

    public ItemData(ItemInfo item, string name, int idImage)
    {
        _item = item;
        _name = name;
        _idImage = idImage;
    }

    public ItemData(ItemInfo item, string name, int idImage, int idPrefab)
    {
        _item = item;
        _name = name;
        _idImage = idImage;
        _idPrefabOnScene = idPrefab;
    }

    public void AddBenefit(ItemBenefit benefit)
    {
        _item.AddBenefit(benefit);
    }

    public void AddBenefit(ItemBenefit[] benefits)
    {
        _item.AddBenefit(benefits);
    }

    public ItemBenefit[] GetBenefits()
    {
        return _item.GetBenefits();
    }

    public void AddRequirement(ItemRequirement requirement)
    {
        _item.AddRequirement(requirement);
    }

    public void AddRequirement(ItemRequirement[] requirements)
    {
        _item.AddRequirement(requirements);
    }
}
