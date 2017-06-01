using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;
using NetworkProject.Requirements;

public interface IRequirement
{
    bool IsRequirementSatisfy(PlayerStats stats);

    void Set(string value);

    string GetAsString();

    IRequirementPackage ToPackage();
}
