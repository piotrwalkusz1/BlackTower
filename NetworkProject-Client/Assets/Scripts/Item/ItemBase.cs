using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using NetworkProject;

[System.CLSCompliant(false)]
static public class ItemBase
{
    static public List<ItemData> _items = new List<ItemData>();
    static public List<ItemData> _weapons = new List<ItemData>();
    static public List<ItemData> _armors = new List<ItemData>();

    static ItemBase()
    {
        LoadItemsFromXmlFile();
    }

    static public void AddItem(ItemData item)
    {
        _items.Add(item);
	}

    static public void AddWeapon(ItemData weapon)
    {
        _weapons.Add(weapon);
    }

    static public void AddArmor(ItemData armor)
    {
        _armors.Add(armor);
    }

    static public ItemData GetAnyItem(int idItem)
    {
        ItemData item = GetItem(idItem);

        if (item != null)
        {
            return item;
        }

        item = GetWeapon(idItem);

        if (item != null)
        {
            return item;
        }

        item = GetArmor(idItem);

        if (item != null)
        {
            return item;
        }

        return null;
    }

    static public ItemData GetItem(int idItem)
    {
        foreach (ItemData item in _items)
        {
            if (item.IdItem == idItem)
            {
                return item;
            }
        }

        return null;
    }

    static public ItemData GetWeapon(int idItem)
    {
        foreach(ItemData weapon in _weapons)
        {
            if (weapon.IdItem == idItem)
            {
                return weapon;
            }
        }

        return null;
    }

    static public ItemData GetArmor(int idItem)
    {
        return _armors.Find(x => x.IdItem == idItem);
    }
	
	static public string GetName( int idItem ) 
    {
        return GetAnyItem(idItem)._name;
	}
	
	static public int GetIdImage( int idItem )
    {
        return GetAnyItem(idItem)._idImage;
	}

    static private void LoadItemsFromXmlFile()
    {
        TextAsset textAsset = Resources.Load<TextAsset>(Settings.pathToItemsInResources);

        XmlDocument document = new XmlDocument();
        document.LoadXml(textAsset.text);
        XmlNode root = document.GetElementsByTagName("items").Item(0);

        foreach (XmlNode item in root.ChildNodes)
        {
            var method = ChooseMethod(item.Name);

            method(item.ChildNodes);
        }
    }

    static private Action<XmlNodeList> ChooseMethod(string type)
    {
        switch (type)
        {
            case "item":
                return AddItemByXmlNodeList;
            case "weapon":
                return AddWeaponByXmlNodeList;
            case "armor":
                return AddArmorByXmlNodeList;
            default:
                throw new Exception("Nie ma takiego typu itemu : " + type);
        }
    }

    static private void AddItemByXmlNodeList(XmlNodeList data)
    {
        ItemInfo itemInfo = new ItemInfo();
        ItemData item = new ItemData(itemInfo);
        
        item.IdItem = int.Parse(data[0].InnerText);
        item._idImage = int.Parse(data[1].InnerText);
        item._idPrefabOnScene = int.Parse(data[2].InnerText);

        AddItem(item);
    }

    static private void AddWeaponByXmlNodeList(XmlNodeList data)
    {
        WeaponInfo weaponInfo = new WeaponInfo();
        ItemData weapon = new ItemData(weaponInfo);
        weapon.IdItem = int.Parse(data[0].InnerText);
        weapon._idImage = int.Parse(data[1].InnerText);
        weapon._idPrefabOnScene = int.Parse(data[2].InnerText);
        weapon._idPrefabOnPlayer = int.Parse(data[3].InnerText);
        weaponInfo._minDmg = int.Parse(data[4].InnerText);
        weaponInfo._maxDmg = int.Parse(data[5].InnerText);
        weaponInfo._attackSpeed = float.Parse(data[6].InnerText);

        AddBenefitsAndRequirements(data, weapon);

        AddWeapon(weapon);
    }

    static private void AddArmorByXmlNodeList(XmlNodeList data)
    {
        var armorInfo = new ArmorInfo();
        var armor = new ItemData(armorInfo);
        armor.IdItem = int.Parse(data[0].InnerText);
        armor._idImage = int.Parse(data[1].InnerText);
        armor._idPrefabOnScene = int.Parse(data[2].InnerText);
        armor._idPrefabOnPlayer = int.Parse(data[3].InnerText);
        armorInfo._defense = int.Parse(data[4].InnerText);

        AddBenefitsAndRequirements(data, armor);

        AddArmor(armor);
    }

    static private void AddBenefitsAndRequirements(XmlNodeList data, ItemData item)
    {
        var benefits = ReadBenefits(data);
        if (benefits.Length > 0)
        {
            item.AddBenefit(benefits);
        }

        var requirements = ReadRequirements(data);
        if (requirements.Length > 0)
        {
            item.AddRequirement(requirements);
        }
    }

    static private ItemBenefit[] ReadBenefits(XmlNodeList data)
    {
        var benefits = new List<ItemBenefit>();

        foreach (XmlNode node in data)
        {
            if (node.Name == "benefit")
            {
                int type = int.Parse(node.ChildNodes[0].InnerText);
                string value = node.ChildNodes[1].InnerText;

                benefits.Add(new ItemBenefit(value, type));
            }
        }

        return benefits.ToArray();
    }

    static private ItemRequirement[] ReadRequirements(XmlNodeList data)
    {
        var requirements = new List<ItemRequirement>();

        foreach (XmlNode node in data)
        {
            if (node.Name == "requirement")
            {
                int type = int.Parse(node.ChildNodes[0].InnerText);
                string value = node.ChildNodes[0].InnerText;

                requirements.Add(new ItemRequirement(type, value));
            }
        }

        return requirements.ToArray();
    }
}
