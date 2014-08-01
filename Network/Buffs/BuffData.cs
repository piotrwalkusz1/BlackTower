using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Buffs
{
    [Serializable]
    public class BuffData
    {
        public int IdBuff { get; set; }

        public BuffData(int idBuff)
        {
            IdBuff = idBuff;
        }
    }
}
