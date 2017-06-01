using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class BuffData
{
    public int IdBuff { get; set; }
    public int IdIcon { get; set; }
    public List<List<IBenefit>> Benefits { get; set; }
    public bool IsVisibleIcon 
    {
        get { return IdIcon != -1; }
    }

    public BuffData()
    {
        Benefits = new List<List<IBenefit>>();
    }

    public BuffData(int idBuff, int idIcon, IBenefit[][] benefits)
    {
        IdBuff = idBuff;
        IdIcon = idIcon;

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
