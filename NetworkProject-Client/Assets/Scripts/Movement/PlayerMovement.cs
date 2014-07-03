using UnityEngine;
using System;
using System.Collections;

[System.CLSCompliant(false)]
public class PlayerMovement : Movement
{
    public float Speed { get; set; }

    protected RaycastHit _hitInfo;

	private Vector3 _targetPosition;
    private bool _isJumpUp;

    private NetPlayer _netPlayer;
    private PlayerAnimation _animation;

    void Start()
    {
        _targetPosition = transform.position;

        _netPlayer = GetComponent<NetPlayer>();
        _animation = GetComponent<PlayerAnimation>();
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, Speed * Time.deltaTime);

        

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
    }

    public override void SetNewRotation(float rotation)
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, rotation, transform.eulerAngles.z);
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

    /*
    private CharacterMotor _characterMotor;
     * 
    void Start()
    {
        _characterMotor = GetComponent<CharacterMotor>();
    }
	
    void Update()
    {
        Vector2 dir = new Vector2(_targetPosition.x - transform.position.x, _targetPosition.z - transform.position.z);
		
        if(dir.sqrMagnitude > 0.125f)
        {
            _characterMotor.inputMoveDirection = new Vector3(dir.x, 0, dir.y);
        }
        else
        {
            _characterMotor.inputMoveDirection = Vector3.zero;
        }	
    }	
	
    public override void SetNewTargetPosition(Vector3 position)
    {
        _targetPosition = position;
		
        Vector3 dir = _targetPosition - transform.position;
		
        if(dir.sqrMagnitude > 16)
        {
            transform.position = position;
        }
    }

    public override void Jump(Vector3 position, Vector3 direction)
    {
        transform.position = position;
        transform.rotation = Quaternion.LookRotation(direction);
		
        StartCoroutine(JumpCoroutine());
    }
	
    IEnumerator JumpCoroutine()
    {
        _characterMotor.inputJump = true;
		
        yield return null;
		
        _characterMotor.inputJump = false;
    }
    */
}
