using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Standard;
using NetworkProject.Items;

[Serializable]
public class UseActionAddHP : IUseAction
{
    public int Value { get; set; }

    public string GetDescription()
    {
        return '+' + Value.ToString() + ' ' + Languages.GetPhrase("hp");
    }

    public string GetValueAsText()
    {
        return Value.ToString();
    }

    public void SetValue(string value)
    {
        Value = int.Parse(value);
    }

    public IUseActionPackage GetPackage()
    {
        return new UseActionAddHPPackage(Value);
    }
}
