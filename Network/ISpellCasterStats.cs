using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NetworkProject
{
    public interface ISpellCasterStats : IStats
    {
        int Lvl { get; }
    }
}
