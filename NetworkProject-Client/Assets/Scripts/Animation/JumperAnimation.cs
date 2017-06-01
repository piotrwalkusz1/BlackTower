using UnityEngine;
using System.Collections;

public class JumperAnimation : MonsterAnimation
{
    public void SetGrounded(bool grounded)
    {
        _animator.SetBool("Grounded", grounded);
    }

    public void Jump()
    {
        StartCoroutine(JumpCoroutine());
    }

    IEnumerator JumpCoroutine()
    {
        _animator.SetBool("Jump", true);

        yield return new WaitForSeconds(0.5f);

        _animator.SetBool("Jump", false);
    }

    public override void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }
}
