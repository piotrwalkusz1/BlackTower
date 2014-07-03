using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using NetworkProject;

public static class SpellRepository
{
    private static List<SpellData> _spells;

    static SpellRepository()
    {
        LoadSpellsFromXmlFile();
        LoadSpellsInfo();
    }

    public static SpellData GetSpellById(int idSpell)
    {
        return _spells.Find(x => x.IdSpell == idSpell);
    }

    public static void LoadSpellsFromXmlFile()
    {
        _spells = new List<SpellData>();

        TextAsset textAsset = Resources.Load<TextAsset>(Settings.pathToSpellsInResources);

        XmlDocument document = new XmlDocument();
        document.LoadXml(textAsset.text);
        XmlNode root = document.GetElementsByTagName("spells").Item(0);

        foreach (XmlNode spell in root.ChildNodes)
        {
            AddSpell(spell);
        }
    }

    public static void LoadSpellsInfo()
    {
        SpellData spell;

        //0
        spell = GetSpellById(0);
        spell.AddInfoToDescription(x => "5");
    }

    private static void AddSpell(XmlNode spellNode)
    {
        XmlNodeList info = spellNode.ChildNodes;

        int idSpell = int.Parse(info[0].InnerText);

        SpellData spell = new SpellData(idSpell);

        for (int i = 1; i < info.Count; i++)
        {
            AddRequirementToSpell(spell, info[i]);
        }

        _spells.Add(spell);
    }

    private static void AddRequirementToSpell(SpellData spell, XmlNode infoNode)
    {
        var func = ChooseMethodGetRequirement(infoNode);

        spell.AddRequirement(func(infoNode));
    }

    private static Func<XmlNode, SpellRequirement> ChooseMethodGetRequirement(XmlNode infoNode)
    {
        switch (infoNode.Name)
        {
            case "lvl":
                return GetLvlRequirement;
            case "cooldown":
                return GetCooldownRequirement;
            default:
                throw new System.Exception("Nie ma takiego wymagania do czaru.");
        }
    }

    private static SpellRequirement GetLvlRequirement(XmlNode requirementNode)
    {
        return new SpellRequirement(int.Parse(requirementNode.InnerText), SpellRequirementType.Lvl);
    }

    private static SpellRequirement GetCooldownRequirement(XmlNode requirementNode)
    {
        return new SpellRequirement(float.Parse(requirementNode.InnerText), SpellRequirementType.TimeCooldown);
    }
}
