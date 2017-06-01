using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Requirements;
using NetworkProject.Benefits;

namespace NetworkProject.Items
{
    public class HelmetPackage : EquipableItemPackage
    {
        public int _defense;

        public HelmetPackage(int idItem, IBenefitPackage[] benefits, IRequirementPackage[] requirements, int defense)
            : base(idItem, benefits, requirements)
        {
            _defense = defense;
        }
    }
}
