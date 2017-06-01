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
    public abstract class EquipableItemPackage : ItemDataPackage
    {
        public List<IBenefitPackage> _benefits = new List<IBenefitPackage>();
        public List<IRequirementPackage> _requirements = new List<IRequirementPackage>();

        public EquipableItemPackage(int idItem, IBenefitPackage[] benefits, IRequirementPackage[] requirements)
            : base(idItem)
        {
            _benefits = new List<IBenefitPackage>(benefits);
            _requirements = new List<IRequirementPackage>(requirements);
        }

        public EquipableItemPackage GetEquipableItemData()
        {
            return this;
        }

        public void AddBenefit(IBenefitPackage benefit)
        {
            _benefits.Add(benefit);
        }

        public void AddBenefit(IBenefitPackage[] benefits)
        {
            _benefits.AddRange(benefits);
        }

        public void AddRequirement(IRequirementPackage requirement)
        {
            _requirements.Add(requirement);
        }

        public void AddRequirement(IRequirementPackage[] requirements)
        {
            _requirements.AddRange(requirements);
        }

        public IBenefitPackage[] GetBenefits()
        {
            return _benefits.ToArray();
        }

        public IRequirementPackage[] GetRequirements()
        {
            return _requirements.ToArray();
        }
    }
}
