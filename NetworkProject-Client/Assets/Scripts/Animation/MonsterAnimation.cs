using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MonsterAnimation : AnimationManager, ICombatAnimation
{
    private Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }

    IEnumerator AttackCoroutine()
    {
        _animator.SetBool("Attack", true);

        yield return new WaitForSeconds(0.1f);

        _animator.SetBool("Attack", false);
    }
}
