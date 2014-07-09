using UnityEngine;
using System.Collections;

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
            _animator.SetFloat("MovingSpeed", 1f);
        }
        else
        {
            _animator.SetFloat("MovingSpeed", 0f);
        }

        CheckLayersWeight();
	}
}
