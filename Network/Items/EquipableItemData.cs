using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Benefits;
using NetworkProject.Requirements;
using NetworkProject.BodyParts;

namespace NetworkProject.Items
{
    [Serializable]
    public abstract class EquipableItemData : ItemData
    {
        protected List<IEquipeBenefit> _benefits = new List<IEquipeBenefit>();
        protected List<IEquipRequirement> _requirements = new List<IEquipRequirement>();

        public EquipableItemData()
        {

        }

        public EquipableItemData(int idItem)
        {
            IdItem = idItem;
        }

        public void ApplyToStats(IEquipableStats stats)
        {
            ApplyItemStats(stats);
            ApplyBenefits(stats);
        }

        protected abstract void ApplyItemStats(IEquipableStats stats);

        public void AddBenefit(IEquipeBenefit benefit)
        {
            _benefits.Add(benefit);
        }

        public void AddBenefit(IEquipeBenefit[] benefits)
        {
            _benefits.AddRange(benefits);
        }

        public void AddRequirement(IEquipRequirement requirement)
        {
            _requirements.Add(requirement);
        }

        public void AddRequirement(IEquipRequirement[] requirements)
        {
            _requirements.AddRange(requirements);
        }

        public IEquipeBenefit[] GetBenefits()
        {
            return _benefits.ToArray();
        }

        public IEquipRequirement[] GetRequirement()
        {
            return _requirements.ToArray();
        }

        public bool CanEquipe(IEquipableStats stats)
        {
            foreach(IEquipRequirement req in _requirements)
            {
                if (!req.IsRequirementSatisfy(stats))
                {
                    return false;
                }
            }

            return true;
        }

        private void ApplyBenefits(IEquipableStats stats)
        {
            foreach (IEquipeBenefit benefit in _benefits)
            {
                benefit.ApplyToStats(stats);
            }
        }
    }
}
