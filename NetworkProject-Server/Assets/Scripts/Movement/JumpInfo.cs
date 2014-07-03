using UnityEngine;
using System.Collections;

[System.CLSCompliant(false)]
public class JumpInfo
{
    public Vector3 Direction { get; private set; }

    public JumpInfo(Vector3 direction)
    {
        Direction = direction;
    }
}
