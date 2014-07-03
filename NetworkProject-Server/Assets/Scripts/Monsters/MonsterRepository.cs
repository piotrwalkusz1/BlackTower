using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using UnityEngine;
using NetworkProject;

public static class MonsterRepository
{
    public static List<MonsterInfo> _monsters;

    static MonsterRepository()
    {
        LoadMonstersFromXmlFile();
    }

    public static MonsterInfo GetMonsterInfo(MonsterType monsterType)
    {
        return _monsters[(int)monsterType];
    }

    public static void LoadMonstersFromXmlFile()
    {
        _monsters = new List<MonsterInfo>();

        TextAsset textAsset = Resources.Load<TextAsset>(Settings.pathToMonstersInResources);

        XmlDocument document = new XmlDocument();
        document.LoadXml(textAsset.text);
        XmlNode root = document.GetElementsByTagName("monsters").Item(0);

        foreach (XmlNode monster in root.ChildNodes)
        {
            AddMonster(monster);
        }
    }

    private static void AddMonster(XmlNode monsterNode)
    {
        MonsterInfo monster = new MonsterInfo();
        XmlNodeList info = monsterNode.ChildNodes;
        monster._id = int.Parse(info[0].InnerText);
        monster._maxHp = int.Parse(info[1].InnerText);
        monster._movingSpeed = int.Parse(info[2].InnerText);

        int i = 3;

        i = AddDamages(i, info, monster);

        i = AddDrops(i, info, monster);

        _monsters.Add(monster);
    }

    private static int AddDamages(int i, XmlNodeList info, MonsterInfo monster)
    {
        for (; ; i++)
        {
            if(info.Count > i && info[i].Name == "dmg")
            {
                monster.AddNewDamage(int.Parse(info[i].InnerText));
            }
            else
            {
                return i;
            }
        }
    }

    private static int AddDrops(int i, XmlNodeList info, MonsterInfo monster)
    {
        for (; ; i++)
        {
            if (info.Count > i && info[i].Name == "drop")
            {
                AddDrop(info[i], monster);
            }
            else
            {
                return i;
            }
        }
    }

    private static void AddDrop(XmlNode drop, MonsterInfo monster)
    {
        int idItem = int.Parse(drop.ChildNodes[0].InnerText);
        float chance = float.Parse(drop.ChildNodes[1].InnerText);

        monster.AddNewDrop(idItem, chance);
    }
}

