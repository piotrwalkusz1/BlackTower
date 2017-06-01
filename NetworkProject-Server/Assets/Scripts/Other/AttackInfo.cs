using UnityEngine;
using System.Collections;

public struct AttackInfo
{
    public IAttacker Attacker
    {
        get
        {
            return _attacker;
        }
    }
    public int Dmg
    {
        get
        {
            return _dmg;
        }
    }
    public DamageType DamageType
    {
        get
        {
            return _damageType;
        }
    }

    private IAttacker _attacker;
    private int _dmg;
    private DamageType _damageType;

    public AttackInfo(IAttacker attacker, int dmg, DamageType damageType)
    {
        _attacker = attacker;
        _dmg = dmg;
        _damageType = damageType;
    }
}
