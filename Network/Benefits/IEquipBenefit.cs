using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Benefits
{
    public interface IEquipBenefit : IBenefit
    {
        void ApplyToStats(IEquipableStats stats);
    }
}
