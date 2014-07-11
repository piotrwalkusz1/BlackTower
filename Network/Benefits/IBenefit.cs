using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Benefits
{
    public interface IBenefit
    {
        void ApplyToStats(IStats stats);

        void Set(string value);

        string GetAsString();
    }
}
