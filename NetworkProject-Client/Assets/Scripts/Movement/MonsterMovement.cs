using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class MonsterMovement : Movement
{
    public int MoveType { get; set; }

    public float[] MovementSpeed { get; set; }

    public float CurrentMovementSpeed
    {
        get { return MovementSpeed[MoveType]; }
    }
}
