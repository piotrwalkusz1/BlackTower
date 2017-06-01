using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class SpeedMonsterAnimation : MonsterAnimation
{
    public void SetSpeed(float speed)
    {
        _animator.SetFloat("Speed", speed);
    }
}
