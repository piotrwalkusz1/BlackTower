using UnityEngine;
using System.Collections;

[System.CLSCompliant(false)]
public class ActionPoint
{
    public Vector3 Position { get; private set; }
    public object Value { get; private set; }
    public ActionPointType Type { get; private set; }

    public ActionPoint(Vector3 position, object value, ActionPointType type)
    {
        Position = position;
        Value = value;
        Type = type;
    }
}
