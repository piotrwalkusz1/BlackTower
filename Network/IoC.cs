using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.BodyParts;
using NetworkProject.Benefits;
using NetworkProject.Requirements;

namespace NetworkProject
{
    public static class IoC
    {
        public static BodyPart GetBodyPart(BodyPartSlot bodyPart)
        {
            switch (bodyPart)
            {
                case BodyPartSlot.Head:
                    return new Head();
                case BodyPartSlot.Chest:
                    return new Chest();
                case BodyPartSlot.Feet:
                    return new Feet();
                case BodyPartSlot.LeftHand:
                    return new LeftHand();
                case BodyPartSlot.RightHand:
                    return new RightHand();
                case BodyPartSlot.Other1:
                    return new Other();
                case BodyPartSlot.Other2:
                    return new Other();
                default:
                    throw new ArgumentException("Nie ma takiej części ciała.");
            }
        }

        public static Benefit GetBenefit(BenefitType benefitType, string value)
        {
            switch (benefitType)
            {
                case BenefitType.AdditionalMaxHp:
                    return new AdditionalMaxHp(value);
                default:
                    throw new Exception("Nie ma takiego benefitu.");
            }
        }

        public static Requirement GetRequirement(RequirementType requirementType, string value)
        {
            switch (requirementType)
            {
                case RequirementType.Lvl:
                    return new RequirementLvl(value);
                default:
                    throw new Exception("Nie ma takiego wymagania.");
            }
        }
    }
}
