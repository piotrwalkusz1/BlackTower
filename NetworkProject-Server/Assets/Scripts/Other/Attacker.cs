using UnityEngine;
using System.Collections;

[System.CLSCompliant(false)]
public class Attacker : IAttacker
{
    public GameObject _attackerObject;

    public Attacker(GameObject attackerObject)
    {
        _attackerObject = attackerObject;
    }
}
