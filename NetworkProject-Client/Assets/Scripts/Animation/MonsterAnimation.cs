using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MonsterAnimation : AnimationManager, ICombatAnimation
{
    public Animator _animator;

    public virtual void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }

    protected IEnumerator AttackCoroutine()
    {
        _animator.SetBool("Attack", true);

        yield return new WaitForSeconds(0.1f);

        _animator.SetBool("Attack", false);
    }
}
