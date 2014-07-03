using UnityEngine;
using System.Collections;

[System.CLSCompliant(false)]
public abstract class NetNamedObject : NetObject
{
    public string Name { get; set; }
}
