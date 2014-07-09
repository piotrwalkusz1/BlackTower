using UnityEngine;
using System.Collections;

[System.CLSCompliant(false)]
public class JumperAnimation : MonoBehaviour
{
    private Animator _animator;

    void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

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
