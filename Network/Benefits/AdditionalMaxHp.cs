using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Benefits
{
    [Serializable]
    public class AdditionalMaxHp : IBuffBenefit, IEquipBenefit
    {
        private int _additionalMaxHp;

        public AdditionalMaxHp()
        {

        }

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
            else if (stats is IBuffBenefit)
            {
                ApplyToStats((IPlayerStats)stats);
            }
        }

        public void ApplyToStats(IPlayerStats stats)
        {
            stats.MaxHP += _additionalMaxHp;
        }

        public void ApplyToStats(IEquipableStats stats)
        {
            stats.MaxHP += _additionalMaxHp;
        }

        public void Set(string value)
        {
            _additionalMaxHp = int.Parse(value);
        }

        public string GetAsString()
        {
            return _additionalMaxHp.ToString();
        }
    }
}
