using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;
using NetworkProject.Benefits;
using NetworkProject.Requirements;
using NetworkProject.BodyParts;
using NetworkProject.Items;

public abstract class EquipableItemData : ItemData
{
    public int IdPrefabOnPlayer { get; set; }
    public List<IBenefit> _benefits = new List<IBenefit>();
    public List<IRequirement> _requirements = new List<IRequirement>();

    public EquipableItemData(int idItem, IBenefit[] benefits, IRequirement[] requirements)
        : base(idItem)
    {
        _benefits = new List<IBenefit>(benefits);
        _requirements = new List<IRequirement>(requirements);
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

    public bool CanEquipe(PlayerStats stats)
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

    public abstract void ApplyItemStats(PlayerStats stats);

    public void ApplyItemBenefits(PlayerStats stats)
    {
        _benefits.ForEach(x => x.ApplyToStats(stats));
    }
}
