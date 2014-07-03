using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Benefits
{
    public abstract class Benefit
    {
        public abstract void ApplyToStats(Stats stats);
    }
}
