using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Standard;
using NetworkProject;
using NetworkProject.Benefits;
using NetworkProject.Requirements;
using NetworkProject.BodyParts;
using NetworkProject.Items;
using NetworkProject.Connection.ToServer;

[Serializable]
public abstract class EquipableItemData : ItemData
{
    public int IdPrefabOnPlayer { get; set; }
    public List<IBenefit> _benefits = new List<IBenefit>();
    public List<IRequirement> _requirements = new List<IRequirement>();

    public EquipableItemData(int idItem)
    {
        IdItem = idItem;
    }

    public EquipableItemData GetEquipableItemData()
    {
        return this;
    }

    public void AddBenefit(IBenefit benefit)
    {
        _benefits.Add(benefit);
    }

    public void AddBenefit(IBenefit[] benefits)
    {
        _benefits.AddRange(benefits);
    }

    public void AddRequirement(IRequirement requirement)
    {
        _requirements.Add(requirement);
    }

    public void AddRequirement(IRequirement[] requirements)
    {
        _requirements.AddRange(requirements);
    }

    public IBenefit[] GetBenefits()
    {
        return _benefits.ToArray();
    }

    public IRequirement[] GetRequirements()
    {
        return _requirements.ToArray();
    }

    public bool CanEquip(PlayerStats stats)
    {
        foreach(IRequirement req in _requirements)
        {
            if (!req.IsRequirementSatisfy(stats))
            {
                return false;
            }
        }

        return true;
    }

    public override string GetDescription()
    {
        string result = Languages.GetItemName(IdItem);

        result += "\n\n";

        result += GetBenefitsDescription();

        return result;
    }

    public override void UseItem(UseItemInfo info)
    {
        var eq = info.Player.GetComponent<OwnPlayerEquipment>();
        var item = (EquipableItemData)eq.GetSlot(info.Slot).ItemData;

        eq.TryEquipItemFromBagAndSendMessage(info.Slot, eq.FindBodyPartForItem(item));
    }

    protected string GetBenefitsDescription()
    {
        string result = "";
        int lenght = _benefits.Count;

        for (int i = 0; i < lenght; i++)
        {
            result += _benefits[i].GetDescription();

            if (i != lenght - 1)
            {
                result += '\n';
            }
        }

        return result;
    }
}
