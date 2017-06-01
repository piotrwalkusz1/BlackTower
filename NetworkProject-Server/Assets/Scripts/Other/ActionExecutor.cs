using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class ActionExecutor : MonoBehaviour
{
    public int _idAction;

    public abstract void ExecuteAction(NetPlayer player);
}
