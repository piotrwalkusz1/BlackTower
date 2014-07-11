using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PlayerHealth : Health
{
    public override void Dead()
    {
        base.Dead();

        GUIController.ShowDeadMessage();
    }
}
