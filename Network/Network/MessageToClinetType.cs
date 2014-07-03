using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject
{
    public enum MessageToClientType
    {
        GoToChoiceCharacterMenu = 0,
        TextMessage,
        GoIntoWorld,

        NewPosition,
        NewRotation,

        Jump,
        Move,
        Rotate,
     
        UpdateItemInEquipment,
        UpdateEquipedItem,
        UpdateHP,
        UpdateMaxHP,
        UpdateAllStats,
        UpdateSpell,
        UpdateAllSpells,
        UpdateExperience,
        UpdateMaxExperience,

        Dead,
        Respawn,
        Attack,

        CreateItem,
        CreateOtherPlayer,
        CreateOwnPlayer,
        CreateVisualObject,
        CreateBullet,
        CreateMonster,

        DeleteObject
    }
}
