using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Benefits;
using NetworkProject.Requirements;

namespace NetworkProject.Items.Repository
{
    [Serializable]
    public abstract class EquipableItem : Item
    {
        protected List<Benefit> _benefits = new List<Benefit>();
        protected List<Requirement> _requirements = new List<Requirement>();

        public EquipableItem()
        {

        }

        public EquipableItem(int idItem)
        {
            IdItem = idItem;
        }

        public void ApplyToStats(Stats stats)
        {
            ApplyItemStats(stats);
            ApplyBenefits(stats);
        }

        public abstract void ApplyItemStats(Stats stats);

        public void SetBenefits(XmlNodeList benefits)
        {
            _benefits = new List<Benefit>();

            foreach (XmlNode benefit in benefits)
            {
                AddBenefit(benefit);
            }
        }

        public void SetRequirements(XmlNodeList requirements)
        {
            foreach (XmlNode requirement in requirements)
            {
                AddRequirement(requirement);
            }
        }     

        public void AddBenefit(Benefit benefit)
        {
            _benefits.Add(benefit);
        }

        public void AddBenefit(Benefit[] benefits)
        {
            _benefits.AddRange(benefits);
        }

        public void AddRequirement(Requirement requirement)
        {
            _requirements.Add(requirement);
        }

        public void AddRequirement(Requirement[] requirements)
        {
            _requirements.AddRange(requirements);
        }

        public Benefit[] GetBenefits()
        {
            return _benefits.ToArray();
        }

        public Requirement[] GetRequirement()
        {
            return _requirements.ToArray();
        }

        private void ApplyBenefits(Stats stats)
        {
            foreach (Benefit benefit in _benefits)
            {
                benefit.ApplyToStats(stats);
            }
        }

        private void AddBenefit(XmlNode benefitNode)
        {
            BenefitType benefitType = (BenefitType)Enum.Parse(typeof(BenefitType), benefitNode.Name, true);

            Benefit benefit = IoC.GetBenefit(benefitType, benefitNode.InnerText);

            _benefits.Add(benefit);
        }

        private void AddRequirement(XmlNode requirementNode)
        {
            RequirementType requirementType = (RequirementType)Enum.Parse(typeof(RequirementType), requirementNode.Name, true);

            Requirement requirement = IoC.GetRequirement(requirementType, requirementNode.InnerText);


            _requirements.Add(requirement);
        }
    }
}
