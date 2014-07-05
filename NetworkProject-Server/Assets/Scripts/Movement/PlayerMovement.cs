using UnityEngine;
using System.Collections;

[System.CLSCompliant(false)]
public class PlayerMovement : Movement
{
    private NetPlayer _netPlayer;

    protected void Start()
    {
        _netPlayer = GetComponent<NetPlayer>();
    }

    virtual public void JumpAndSendMessage()
    {
        _netPlayer.SendJumpMessage();
    }
}
