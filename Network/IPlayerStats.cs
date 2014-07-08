using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject
{
    public interface IPlayerStats : IEquipableStats
    {
        BreedAndGender BreedAndGender { get; }
        int HP { get; set; }
    }
}
