﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class UseItemInfo
{
    public GameObject Player { get; set; }
    public int Slot { get; set; }

    public UseItemInfo(GameObject player, int slot)
    {
        Player = player;
        Slot = slot;
    }
}
