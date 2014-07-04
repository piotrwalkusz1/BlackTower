using UnityEngine;
using System.Collections;
using NetworkProject;

[System.CLSCompliant(false)]
public class NetMonster : NetObject
{
    private bool _isJumpMessage;
    private bool _isJumpMessageInLastFrame;

    private bool _isAttackMessage;
    private bool _isAttackMessageInLastFrame;
    private int _idAttackVictim;

    private bool _isAllStatsMessage;
    private bool _isAllStatsMessageInLastFrame;

    new protected void Awake()
    {
        VisionFunctionToDefault();

        InitializePositionAndRotation();
    }

    new protected void LateUpdate()
    {
        MessageFlagsToDefault();
    }

    public override bool IsMustUpdate()
    {
        return (base.IsMustUpdate() || _isJumpMessageInLastFrame || _isAttackMessageInLastFrame || _isAllStatsMessageInLastFrame);
    }

    public override void SendMessageAppeared(IConnectionMember address)
    {
        MonsterPackage monster = ToMonsterPackages();

        Server.SendMessageCreateMonster(monster, address);
    }

    public override void SendMessageUpdate(IConnectionMember address)
    {
        IfChangeSendPositionUpdate(address);

        IfChangeSendRotationUpdate(address);

        if (!IfFlagSendAllStatsMessage(address))
        {
            IfFlagSendHpUpdate(address);

            IfFlagSendMaxHpUpdate(address);
        }

        IfFlagSendAttackEvent(address);

        IfFlagSendJumpEvent(address);
    }

    public override void SendMessageDisappeared(IConnectionMember address)
    {
        Server.SendMessageDeleteObject(IdNet, address);
    }

    public void SendJumpMessage()
    {
        _isJumpMessage = true;
    }

    public void SendAttackMessage(int idAttackVictim)
    {
        _isAttackMessage = true;
        _idAttackVictim = idAttackVictim;
    }

    public void SendAllStatsMessage()
    {
        _isAllStatsMessage = true;
    }

    new protected void MessageFlagsToDefault()
    {
        base.MessageFlagsToDefault();

        _isAttackMessageInLastFrame = _isAttackMessage;
        _isAttackMessage = false;
        _idAttackVictim = -1;

        _isJumpMessageInLastFrame = _isJumpMessage;
        _isJumpMessage = false;

        _isAllStatsMessageInLastFrame = _isAllStatsMessage;
        _isAllStatsMessage = false;
    }

    protected void IfFlagSendAttackEvent(IConnectionMember address)
    {
        if (_isAttackMessageInLastFrame)
        {
            Server.SendMessageMonsterAttackTarget(IdNet, _idAttackVictim, address);
        }
    }

    protected void IfFlagSendJumpEvent(IConnectionMember address)
    {
        if (_isJumpMessageInLastFrame)
        {
            Server.SendMessageJump(IdNet, transform.position, Vector3.zero, address);
        }
    }

    protected bool IfFlagSendAllStatsMessage(IConnectionMember address)
    {
        if (_isAllStatsMessageInLastFrame)
        {
            StatsPackage package = GetComponent<MonsterStats>().GetMonsterStatsPackage();

            Server.SendMessageUpdateOtherAllStats(IdNet, package, address);
        }

        return _isAllStatsMessage;
    }

    protected MonsterPackage ToMonsterPackages()
    {
        MonsterPackage monster = new MonsterPackage();
        monster.IdObject = IdNet;
        monster._position = transform.position;
        monster._rotation = transform.eulerAngles.y;
        monster._monsterType = MonsterType.Jumper;
        monster._stats = GetComponent<JumperStats>().GetMonsterStatsPackage();

        return monster;
    }
}
