using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using NetworkProject.Benefits;
using NetworkProject.Requirements;

namespace NetworkProject.Items
{
    [Serializable]
    public class AdditionPackage : EquipableItemPackage
    {
        public AdditionPackage(int idItem, IBenefitPackage[] benefits, IRequirementPackage[] requirements)
            : base(idItem, benefits, requirements)
        {

        }
    }
}
