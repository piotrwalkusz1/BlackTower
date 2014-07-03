using UnityEngine;
using System.Collections;

[System.CLSCompliant(false)]
public class OtherPlayerAnimation : PlayerAnimation
{
    private NetPlayer _netPlayer;

	void Start() 
    {
        _netPlayer = GetComponent<NetPlayer>();
	}
	
	void Update() 
    {
        if (_netPlayer.IsMovement())
        {
            animator.SetFloat("MovingSpeed", 1f);
        }
        else
        {
            animator.SetFloat("MovingSpeed", 0f);
        }

        CheckLayersWeight();
	}
}
