using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Benefits;

namespace NetworkProject.Buffs
{
    [Serializable]
    public class BuffDataPackage
    {
        public int IdBuff { get; set; }
        public List<List<IBenefitPackage>> Benefits { get; set; }

        public BuffDataPackage()
        {
            Benefits = new List<List<IBenefitPackage>>();
        }

        public BuffDataPackage(int idBuff, IBenefitPackage[][] benefits)
        {
            IdBuff = idBuff;
            
            Benefits = new List<List<IBenefitPackage>>();

            foreach (var benefitsByLvl in benefits)
            {
                Benefits.Add(benefitsByLvl.ToList());
            }
        }

        public IBenefitPackage[][] GetBenefits()
        {
            List<IBenefitPackage[]> benefits = new List<IBenefitPackage[]>();

            foreach (var benefitsByLvl in Benefits)
            {
                benefits.Add(benefitsByLvl.ToArray());
            }

            return benefits.ToArray();
        }
    }
}
