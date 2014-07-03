using UnityEngine;
using System;
using System.Collections;

[System.CLSCompliant(false)]
public class OwnPlayerAnimation : PlayerAnimation
{
    private CharacterMotor _motor;

    private Vector2 _velocity;

    void Start()
    {
        _motor = GetComponent<CharacterMotor>();    
    }

	void Update ()
    {
        _velocity = new Vector2(_motor.movement.velocity.x, _motor.movement.velocity.z);
        
        animator.SetFloat("MovingSpeed", _velocity.magnitude);

        CheckLayersWeight();
	}
}
