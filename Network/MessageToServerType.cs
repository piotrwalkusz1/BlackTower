using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject
{
    public enum MessageToServerType
    {
        Login = 0,
        Register,
        GoIntoWorld,
        PlayerMove,
        PlayerJump,
        PlayerRotation,
        PickItem,
        Attack,
        Respawn,
        ChangeItemsInEquipment,
        ChangeEquipedItem,
        ChangeEquipedItems,
        UseSpell
    }
}
