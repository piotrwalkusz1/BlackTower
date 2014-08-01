using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PlayerHealth : Health
{
    public override int HP
    {
        get
        {
            return base.HP;
        }
        set
        {
            base.HP = value;

            if (GetComponent<NetOwnPlayer>() != null)
            {
                GUIController.IfActiveCharacterGUIRefresh();
            }
        }
    }

    public override void Dead()
    {
        base.Dead();

        if (GetComponent<NetOwnPlayer>() != null)
        {
            GUIController.ShowDeadMessage();
        }
    }
}
