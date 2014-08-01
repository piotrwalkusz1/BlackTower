using UnityEngine;
using System.Collections;

public class Attacker : IAttacker
{
    public GameObject _attackerObject;

    public Attacker(GameObject attackerObject)
    {
        _attackerObject = attackerObject;
    }
}
