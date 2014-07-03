using UnityEngine;
using System.Collections;

[System.CLSCompliant(false)]
public class NetMonster : NetObject
{
    public MonsterCombat Combat { get; private set; }
    protected Vector3 _lastPosition;

    protected new void Awake()
    {
        Combat = GetComponent<MonsterCombat>();
    }

    void LateUpdate()
    {
        _lastPosition = transform.position;
    }

    public bool IsMovement()
    {
        return transform.position != _lastPosition;
    }

    public bool IsFall()
    {
        return transform.position.y < _lastPosition.y;
    }
}
