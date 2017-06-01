using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ItemUsableData : ItemData
{
    public List<IUseAction> Actions { get; set; }

    public ItemUsableData(int idItem, IUseAction[] actions)
        : base(idItem)
    {
        Actions = new List<IUseAction>(actions);
    }

    public void Use(GameObject player)
    {
        Actions.ForEach(x => x.Use(player));
    }
}
