using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Benefits
{
    public class AdditionalMaxHp : Benefit
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

        public override void ApplyToStats(Stats stats)
        {
            stats.MaxHP += _additionalMaxHp;
        }
    }
}
