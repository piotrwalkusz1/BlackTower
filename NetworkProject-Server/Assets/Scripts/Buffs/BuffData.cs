using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class BuffData
{
    public int IdBuff { get; set; }
    public List<List<IBenefit>> Benefits { get; set; }

    public BuffData(int idBuff, IBenefit[][] benefits)
    {
        IdBuff = idBuff;

        Benefits = new List<List<IBenefit>>();

        foreach (var benefitsByLvl in benefits)
        {
            Benefits.Add(benefitsByLvl.ToList());
        }
    }

    public IBenefit[][] GetBenefits()
    {
        List<IBenefit[]> benefits = new List<IBenefit[]>();

        foreach (var benefitsByLvl in Benefits)
        {
            benefits.Add(benefitsByLvl.ToArray());
        }

        return benefits.ToArray();
    }
}
