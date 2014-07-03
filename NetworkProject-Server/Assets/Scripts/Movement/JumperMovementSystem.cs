using UnityEngine;
using System.Collections;

[System.CLSCompliant(false)]
public class JumperMovementSystem : MovementSystem
{
    public float _moveSpeed;

    public float _jumpHeight;
    public float _halfHeight;

    private Vector3 _jumpSpeed;
    private bool _isJump = false;

    private Vector3 _moveTargetPosition;

    private float Gravity
    {
        get
        {
            return -Physics.gravity.y;
        }
    }

    private CharacterController _characterController;

    protected void Awake()
    {
        _moveTargetPosition = transform.position;
        _characterController = GetComponent<CharacterController>();
    }

    protected void Update()
    {
        ApplyGravity(ref _jumpSpeed); 

        Vector3 movement = Vector3.zero;

        if (!IsStop())
        {
            RotateToTarget();

            movement += transform.TransformDirection(Vector3.forward * _moveSpeed);
        }

        if (_jumpSpeed.y < 0 && IsGrounded())
        {
            _isJump = false;
            _jumpSpeed = Vector3.zero;
        }     

        movement += _jumpSpeed;

        movement *= Time.deltaTime;

        _characterController.Move(movement); 
    }

    public override void SetNewPosition(Vector3 newPosition)
    {
        _moveTargetPosition = newPosition;
    }

    public virtual bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, _halfHeight);
    }

    public virtual void Jump(Vector3 position, Vector3 direction)
    {
        _isJump = true;

        transform.position = position;

        _jumpSpeed = new Vector3(direction.x, CalculateJumpSpeed(), direction.z);
    }

    public bool IsJump()
    {
        return _isJump;
    }

    public bool IsStop()
    {
        return transform.position.x == _moveTargetPosition.x && transform.position.z == _moveTargetPosition.z;
    }

    public override void Stop()
    {
        _moveTargetPosition = transform.position;
    }

    protected float CalculateJumpSpeed()
    {
        return Mathf.Sqrt(2 * _jumpHeight * Gravity);
    }

    protected void ApplyGravity(ref Vector3 direction)
    {
        direction += Physics.gravity * Time.deltaTime;
    }

    protected void RotateToTarget()
    {
        Vector3 newDir = _moveTargetPosition - transform.position;

        newDir.y = 0;

        transform.rotation = Quaternion.LookRotation(newDir);
    }
}