using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Benefits;
using NetworkProject.Requirements;

namespace NetworkProject.Items
{
    [Serializable]
    public class WeaponPackage : EquipableItemPackage
    {
        public int _minDmg;
        public int _maxDmg;
        public int _attackSpeed;

        public WeaponPackage(int idItem, IBenefitPackage[] benefits, IRequirementPackage[] requirements, int minDmg,
            int maxDmg, int attackSpeed)
            : base(idItem, benefits, requirements)
        {
            _minDmg = minDmg;
            _maxDmg = maxDmg;
            _attackSpeed = attackSpeed;
        }
    }
}
