using UnityEngine;
using System;
using System.Collections;

[System.CLSCompliant(false)]
public class PlayerAnimation : MonoBehaviour
{
    public Animator animator;

    public void SetGrounded(bool grounded)
    {
        animator.SetBool("Grounded", grounded);
    }

    protected DateTime _rightHandLayerEndTime = DateTime.UtcNow;

    protected float _attack1AnimationLength = 0.5f;

    protected void OnFall()
    {
        animator.SetBool("Grounded", false);
    }

    protected void OnLand()
    {
        animator.SetBool("Grounded", true);
    }

    protected void OnJump()
    {
        StartCoroutine(JumpCoroutine());

        animator.SetBool("Grounded", false);
    }

    protected void OnAttack1()
    {
        StartCoroutine(Attack1AnimationStartCoroutine());

        RightHandLayerStart(DateTime.UtcNow.AddSeconds(_attack1AnimationLength));
    }

    protected void RightHandLayerStart(DateTime endTime)
    {
        if (endTime > _rightHandLayerEndTime)
        {
            _rightHandLayerEndTime = endTime;
        }
    }

    protected IEnumerator Attack1AnimationStartCoroutine()
    {
        animator.SetBool("Attack1", true);

        yield return null;

        animator.SetBool("Attack1", false);

    }

    protected void CheckLayersWeight()
    {
        if (IsActiveRightHandLayer())
        {
            animator.SetLayerWeight(1, Mathf.Clamp(animator.GetLayerWeight(1) + 10f * Time.deltaTime, 0, 1));
        }
        else
        {
            animator.SetLayerWeight(1, Mathf.Clamp(animator.GetLayerWeight(1) - 10f * Time.deltaTime, 0, 1));
        }
    }

    protected bool IsActiveRightHandLayer()
    {
        return (_rightHandLayerEndTime > DateTime.UtcNow);
    }

    IEnumerator JumpCoroutine()
    {
        animator.SetBool("Jump", true);

        yield return new WaitForSeconds(0.5f);

        animator.SetBool("Jump", false);
    }
}
