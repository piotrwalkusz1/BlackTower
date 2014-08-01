using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Items;
using UnityEditor;

namespace EditorExtension
{
    public class ItemWindowShieldData : ItemWindowEquipableData
    {
        public int Defense
        {
            get { return ((ShieldData)VisualEquipableItem.EquipableItemData)._defense; }
            set { ((ShieldData)VisualEquipableItem.EquipableItemData)._defense = value; }
        }

        public ItemWindowShieldData(ShieldData shield) : base(shield)
        {

        }

        public ItemWindowShieldData(VisualEquipableItemData shield) : base(shield)
        {
            Defense = ((ShieldData)shield.EquipableItemData)._defense;
        }

        public override void Draw()
        {
            base.Draw();

            Defense = EditorGUILayout.IntField("Defense", Defense);
        }

        public override string GetItemTypeName()
        {
            return "Shield";
        }

    }
}
