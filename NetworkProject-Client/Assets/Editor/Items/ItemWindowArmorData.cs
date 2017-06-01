using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Items;
using UnityEditor;

namespace EditorExtension
{
    public class ItemWindowArmorData : ItemWindowEquipableData
    {
        public int Defense
        {
            get { return ((ArmorData)Item)._defense; }
            set { ((ArmorData)Item)._defense = value; }
        }

        public ItemWindowArmorData(ArmorData armor) : base(armor)
        {

        }

        public override void Draw()
        {
            base.Draw();

            Defense = EditorGUILayout.IntField("Defense", Defense);
        }

        public override string GetItemTypeName()
        {
            return "Armor";
        }
    }
}
