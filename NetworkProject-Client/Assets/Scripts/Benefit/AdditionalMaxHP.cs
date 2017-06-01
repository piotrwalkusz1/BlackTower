using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Standard;
using NetworkProject.Benefits;

[Serializable]
public class AdditionalMaxHP : IBenefit
{
    public int Value { get; set; }

    public AdditionalMaxHP()
    {

    }

    public AdditionalMaxHP(int value)
    {
        Value = value;
    }

    public void Set(string value)
    {
        Value = int.Parse(value);
    }

    public string GetValueAsString()
    {
        return Value.ToString();
    }

    public IBenefitPackage ToPackage()
    {
        return new AdditionalMaxHpPackage(Value);
    }


    public string GetDescription()
    {
        return '+' + GetValueAsString() + ' ' + Languages.GetPhrase("maxHP");
    }
}
