using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Benefits;

public interface IBenefit
{
    void Set(string value);

    string GetValueAsString();

    IBenefitPackage ToPackage();

    string GetDescription();
}
