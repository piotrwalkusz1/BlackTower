using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Benefits;
using NetworkProject.Requirements;

namespace NetworkProject.Items
{
    [Serializable]
    public class ShieldPackage : EquipableItemPackage
    {
        public int _defense;

        public ShieldPackage(int idItem, IBenefitPackage[] benefits, IRequirementPackage[] requirements, int defense)
            : base(idItem, benefits, requirements)
        {
            _defense = defense;
        }
    }
}
