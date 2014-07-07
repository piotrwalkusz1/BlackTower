using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Benefits
{
    [Serializable]
    public class AdditionalMaxHp : IBuffBenefit, IEquipeBenefit
    {
        private int _additionalMaxHp;

        public AdditionalMaxHp(string value)
        {
            _additionalMaxHp = int.Parse(value);
        }

        public AdditionalMaxHp(int additionalMaxHp)
        {
            _additionalMaxHp = additionalMaxHp;
        }

        public void ApplyToStats(IStats stats)
        {
            if (stats is IEquipableStats)
            {
                ApplyToStats((IEquipableStats)stats);
            }
        }

        public void ApplyToStats(IPlayerStats stats)
        {
            ApplyToStats((IEquipableStats)stats);
        }

        public void ApplyToStats(IEquipableStats stats)
        {
            stats.MaxHP += _additionalMaxHp;
        }
    }
}
