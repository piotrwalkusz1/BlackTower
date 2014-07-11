using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Requirements
{
    public interface IRequirement
    {
        bool IsRequirementSatisfy(IStats stats);

        void Set(string value);

        string GetAsString();
    }
}
