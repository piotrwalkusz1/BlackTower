using UnityEngine;
using System.Collections;
using NetworkProject.Connection.ToClient;

public class PlayerMovement : Movement
{
    private bool _isIgnoreSetPosition;

    private NetPlayer _netPlayer;

    protected void Start()
    {
        _netPlayer = GetComponent<NetPlayer>();
    }

    public override void SetNewPosition(Vector3 newPosition)
    {
        if (!_isIgnoreSetPosition)
        {
            base.SetNewPosition(newPosition);
        }
    }

    virtual public void JumpAndSendMessage()
    {
        _netPlayer.SendJumpMessage();
    }

    public void SendUpdatePositionToOwnerAndWaitForResponse()
    {
        _isIgnoreSetPosition = true;

        var request = new MoveYourCharacterToClient(transform.position);

        Server.SendRequestAsMessage(request, GetComponent<NetPlayer>().OwnerAddress);
    }

    public void ResponseUpdatePosition()
    {
        _isIgnoreSetPosition = false;
    }
}
