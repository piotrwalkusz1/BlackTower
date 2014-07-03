using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using UnityEngine;
using NetworkProject;

[System.CLSCompliant(false)]
public static class ItemRepository
{
    private static List<ItemInfo> _items;

    static ItemRepository()
    {
        LoadItemsFromXmlFile();
    }

    public static ItemInfo GetItem(int idItem)
    {
        foreach (ItemInfo item in _items)
        {
            if (item._idItem == idItem)
            {
                return item;
            }
        }

        return null;
    }

    public static WeaponInfo GetWeapon(int idItem)
    {
        return (WeaponInfo)GetItem(idItem);
    }

    public static ArmorInfo GetArmor(int idItem)
    {
        return (ArmorInfo)GetItem(idItem);
    }

    public static void AddItem(ItemInfo item)
    {
        _items.Add(item);
    }

    public static void LoadItemsFromXmlFile()
    {
        _items = new List<ItemInfo>();

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
        ItemInfo item = new ItemInfo();

        item._idItem = int.Parse(data[0].InnerText);

        AddItem(item);
    }

    static private void AddWeaponByXmlNodeList(XmlNodeList data)
    {
        WeaponInfo weapon = new WeaponInfo();
        weapon._idItem = int.Parse(data[0].InnerText);
        weapon._minDmg = int.Parse(data[1].InnerText);
        weapon._maxDmg = int.Parse(data[2].InnerText);
        weapon._attackSpeed = float.Parse(data[3].InnerText);

        AddBenefitsAndRequirements(data, weapon);

        AddWeapon(weapon);
    }

    static private void AddArmorByXmlNodeList(XmlNodeList data)
    {
        var armor = new ArmorInfo();
        armor._idItem = int.Parse(data[0].InnerText);
        armor._defense = int.Parse(data[1].InnerText);

        AddBenefitsAndRequirements(data, armor);

        AddArmor(armor);
    }

    static private void AddBenefitsAndRequirements(XmlNodeList data, ItemInfo item)
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
