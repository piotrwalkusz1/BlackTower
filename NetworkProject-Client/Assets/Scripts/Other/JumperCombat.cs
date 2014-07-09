using UnityEngine;
using System.Collections;

public class JumperCombat : MonsterCombat
{
    private JumperAnimation _animation;

    void Awake()
    {
        _animation = GetComponent<JumperAnimation>();
    }

    public override void AttackTarget(NetObject target)
    {
        _animation.Attack();
    }
}
