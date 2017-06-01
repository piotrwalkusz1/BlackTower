using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Benefits;
using NetworkProject.Requirements;

namespace NetworkProject.Items
{
    [Serializable]
    public class ShoesPackage : EquipableItemPackage
    {
        public float _movementSpeed;

        public ShoesPackage(int idItem, IBenefitPackage[] benefits, IRequirementPackage[] requirements, float movementSpeed)
            : base(idItem, benefits, requirements)
        {
            _movementSpeed = movementSpeed;
        }
    }
}
