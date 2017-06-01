using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class UseActionAddHP : IUseAction
{
    public int Value { get; set; }

    public UseActionAddHP(int value)
    {
        Value = value;
    }

    public void Use(GameObject player)
    {
        player.GetComponent<PlayerHealthSystem>().IncreaseHPAndSendUpdate(Value);
    }
}
