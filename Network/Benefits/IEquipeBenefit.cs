using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Benefits
{
    public interface IEquipeBenefit : IBenefit
    {
        void ApplyToStats(IEquipableStats stats);
    }
}
