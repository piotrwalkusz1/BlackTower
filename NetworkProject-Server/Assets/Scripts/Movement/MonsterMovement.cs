using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class MonsterMovement : Movement
{
    public float MoveSpeed
    {
        get { return MovementSpeedByType[MovementType]; }
    }

    public float[] MovementSpeedByType { get; set; }

    public int MovementType { get; set; }
}
