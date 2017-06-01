using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Items
{
    [Serializable]
    public class UseActionAddHPPackage : IUseActionPackage
    {
        public int Value { get; set; }

        public UseActionAddHPPackage(int value)
        {
            Value = value;
        }
    }
}
