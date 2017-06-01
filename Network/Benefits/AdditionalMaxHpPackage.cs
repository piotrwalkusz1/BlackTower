using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Benefits
{
    [Serializable]
    public class AdditionalMaxHpPackage : IBenefitPackage
    {
        public int Value { get; set; }

        public AdditionalMaxHpPackage()
        {
            Value = 0;
        }

        public AdditionalMaxHpPackage(int additionalMaxHp)
        {
            Value = additionalMaxHp;
        }
    }
}
