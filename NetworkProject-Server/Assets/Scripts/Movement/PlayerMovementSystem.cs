using UnityEngine;
using System.Collections;

[System.CLSCompliant(false)]
public class PlayerMovementSystem : MovementSystem
{
    private NetPlayer _netPlayer;

    protected void Start()
    {
        _netPlayer = GetComponent<NetPlayer>();
    }

    virtual public void Jump(Vector3 position, Vector3 direction)
    {
        _netPlayer.SendJumpMessage();
    }
}
