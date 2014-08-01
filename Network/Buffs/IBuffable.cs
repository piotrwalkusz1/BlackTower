using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NetworkProject.Buffs
{
    public interface IBuffable
    {
        void AddBuff(Buff buff);
    } 
}
