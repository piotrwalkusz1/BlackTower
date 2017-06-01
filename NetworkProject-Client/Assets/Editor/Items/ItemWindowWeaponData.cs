using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Items;
using UnityEditor;

namespace EditorExtension
{
    public class ItemWindowWeaponData : ItemWindowEquipableData
    {
        public int MinDmg
        {
            get { return ((WeaponData)Item)._minDmg; }
            set { ((WeaponData)Item)._minDmg = value; }
        }
        public int MaxDmg
        {
            get { return ((WeaponData)Item)._maxDmg; }
            set { ((WeaponData)Item)._maxDmg = value; }
        }
        public int AttackSpeed
        {
            get { return ((WeaponData)Item)._attackSpeed; }
            set { ((WeaponData)Item)._attackSpeed = value; }
        }

        public ItemWindowWeaponData(WeaponData weapon) : base(weapon)
        {

        }

        public override void Draw()
        {
            base.Draw();

            MinDmg = EditorGUILayout.IntField("Min damage", MinDmg);
            MaxDmg = EditorGUILayout.IntField("Max damage", MaxDmg);
            AttackSpeed = EditorGUILayout.IntField("Attack speed", AttackSpeed);
        }

        public override string GetItemTypeName()
        {
            return "Weapon";
        }
    }
}
