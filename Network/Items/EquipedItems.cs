using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Items
{
    public abstract class EquipedItems
    {
        public abstract void ApplyToStats(IEquipableStats stats);
    }
}
