﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject
{
    public interface IMonsterStats : IStats
    {
        float AttackSpeed { get; set; }
    }
}