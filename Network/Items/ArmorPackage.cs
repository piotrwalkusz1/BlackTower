using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Requirements;
using NetworkProject.Benefits;

namespace NetworkProject.Items
{
    [Serializable]
    public class ArmorPackage : EquipableItemPackage
    {
        public int _defense;

        public ArmorPackage(int idItem, IBenefitPackage[] benefits, IRequirementPackage[] requirements, int defense)
            : base(idItem, benefits, requirements)
        {
            _defense = defense;
        }
    }
}
