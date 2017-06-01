using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Items;

public interface IUseAction
{
    string GetDescription();

    string GetValueAsText();

    void SetValue(string value);

    IUseActionPackage GetPackage();
}
