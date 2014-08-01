﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NetworkProject.Quests
{
    public interface IQuestTarget
    {
        bool IsComplete();

        IQuestTarget GetCopy();
    }
}