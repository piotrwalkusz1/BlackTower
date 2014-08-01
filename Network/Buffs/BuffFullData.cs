using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Benefits;

namespace NetworkProject.Buffs
{
    [Serializable]
    public class BuffFullData : BuffData
    {
        public bool IsTransformBuff { get; set; }

        public BuffFullData(int idBuff)
            : base(idBuff)
        {

        }
    }
}
