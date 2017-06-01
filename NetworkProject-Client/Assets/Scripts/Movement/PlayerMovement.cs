using UnityEngine;
using System;
using System.Collections;



public class PlayerMovement : Movement
{
    private enum PlayerMovementStatus
    {
        ToTarget,
        Slowdown
    }

    public float Speed { get; set; }

    protected RaycastHit _hitInfo;

    private PlayerMovementStatus _status = PlayerMovementStatus.ToTarget;
    private float _slowdownMovementCurrentSpeed;
    private float _slowdownMovementStartSpeed;
    private Vector3 _movementDirection;
    private Vector3 _targetPosition;
    private float _targetRotation;   
    private bool _isJumpUp;
    private const float SPEED_ROTATION = 720f;
    private const float SLOWDOWN_TIME = 0.2f;
    private const float START_SLOWDOWN_SPEED_RATE = 0.2f;

    private NetPlayer _netPlayer;
    private PlayerAnimation _animation;

    void Start()
    {
        _targetPosition = transform.position;
        _movementDirection = Vector3.zero;

        _netPlayer = GetComponent<NetPlayer>();
        _animation = GetComponent<PlayerAnimation>();
    }

    void Update()
    {
        if (_status == PlayerMovementStatus.ToTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, Speed * Time.deltaTime);

            if (transform.position == _targetPosition)
            {
                _status = PlayerMovementStatus.Slowdown;
                _slowdownMovementStartSpeed = Speed * START_SLOWDOWN_SPEED_RATE;
                _slowdownMovementCurrentSpeed = _slowdownMovementStartSpeed;
            }
        }

        if (_status == PlayerMovementStatus.Slowdown)
        {
            if (_slowdownMovementCurrentSpeed != 0)
            {
                transform.Translate(_movementDirection * _slowdownMovementCurrentSpeed * Time.deltaTime);

                _slowdownMovementCurrentSpeed = Mathf.MoveTowards(_slowdownMovementCurrentSpeed, 0f, _slowdownMovementStartSpeed / SLOWDOWN_TIME * Time.deltaTime);
            }
        }   

        transform.eulerAngles = new Vector3(0, Mathf.MoveTowardsAngle(transform.eulerAngles.y, _targetRotation,
            SPEED_ROTATION * Time.deltaTime), 0);

        if (_isJumpUp && _netPlayer.IsFall())
        {
            _isJumpUp = false;
        }

        if (!_isJumpUp)
        {
            _animation.SetGrounded(CheckGrounded());
        }
    }

    public override void SetNewTargetPosition(Vector3 position)
    {
        _targetPosition = position;
        _movementDirection = (position - transform.position).normalized;
        _status = PlayerMovementStatus.ToTarget;
    }

    public override void SetNewRotation(float rotation)
    {
        _targetRotation = rotation;
    }

    public override void Jump(Vector3 position, Vector3 direction)
    {
 	    transform.position = position;

        _isJumpUp = true;

        SendMessage("OnJump");
    }

    public bool CheckGrounded()
    {
        if(Physics.Raycast(transform.position + Vector3.down * Standard.Settings.playerHalfHeight, Vector3.down, out _hitInfo, Standard.Settings.playerDistanceToGround))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
