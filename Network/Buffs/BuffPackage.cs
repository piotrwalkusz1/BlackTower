using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Buffs
{
    [Serializable]
    public class BuffPackage
    {
        public int IdBuff { get; set; }
        public int LvlBuff { get; set; }
        public DateTime EndTime { get; set; }

        public BuffPackage(int idBuff, int lvlBuff, DateTime endTime)
        {
            IdBuff = idBuff;
            LvlBuff = lvlBuff;
            EndTime = endTime;
        }
    }
}
