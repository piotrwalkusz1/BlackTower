using UnityEngine;
using System;
using System.Collections;

public class PlayerAnimation : AnimationManager, ICombatAnimation
{
    protected Animator _animator;

    protected DateTime _rightHandLayerEndTime = DateTime.UtcNow;

    protected float _attackAnimationLength = 0.5f;

    public void SetAnimator()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void SetGrounded(bool grounded)
    {
        _animator.SetBool("Grounded", grounded);
    }

    public void Attack()
    {
        StartCoroutine(AttackAnimationStartCoroutine());

        RightHandLayerStart(DateTime.UtcNow.AddSeconds(_attackAnimationLength));
    }

    protected void OnFall()
    {
        _animator.SetBool("Grounded", false);
    }

    protected void OnLand()
    {
        _animator.SetBool("Grounded", true);
    }

    protected void OnJump()
    {
        StartCoroutine(JumpCoroutine());

        _animator.SetBool("Grounded", false);
    } 

    protected void RightHandLayerStart(DateTime endTime)
    {
        if (endTime > _rightHandLayerEndTime)
        {
            _rightHandLayerEndTime = endTime;
        }
    }

    protected IEnumerator AttackAnimationStartCoroutine()
    {
        _animator.SetBool("Attack", true);

        yield return null;

        _animator.SetBool("Attack", false);

    }

    protected void CheckLayersWeight()
    {
        if (IsActiveRightHandLayer())
        {
            _animator.SetLayerWeight(1, Mathf.Clamp(_animator.GetLayerWeight(1) + 10f * Time.deltaTime, 0, 1));
        }
        else
        {
            _animator.SetLayerWeight(1, Mathf.Clamp(_animator.GetLayerWeight(1) - 10f * Time.deltaTime, 0, 1));
        }
    }

    protected bool IsActiveRightHandLayer()
    {
        return (_rightHandLayerEndTime > DateTime.UtcNow);
    }

    IEnumerator JumpCoroutine()
    {
        _animator.SetBool("Jump", true);

        yield return new WaitForSeconds(0.5f);

        _animator.SetBool("Jump", false);
    }
}
