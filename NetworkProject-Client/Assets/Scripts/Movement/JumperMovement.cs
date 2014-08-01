using UnityEngine;
using System.Collections;

public class JumperMovement : MonsterMovement
{
    protected RaycastHit _hitInfo;

    private Vector3 _targetPosition;
    private bool _isJumpUp;

    private NetMonster _netMonster;
    private JumperAnimation _animation;

    void Start()
    {
        _targetPosition = transform.position;

        _netMonster = GetComponent<NetMonster>();
        _animation = GetComponent<JumperAnimation>();
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, MovementSpeed * Time.deltaTime);

        if (_isJumpUp && _netMonster.IsFall())
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
        if (Physics.Raycast(transform.position + Vector3.down * Standard.Settings.playerHalfHeight, Vector3.down, out _hitInfo, Standard.Settings.playerDistanceToGround))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
