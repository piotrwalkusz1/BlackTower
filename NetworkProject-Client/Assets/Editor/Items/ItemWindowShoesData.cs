using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Items;
using UnityEditor;

namespace EditorExtension
{
    public class ItemWindowShoesData : ItemWindowEquipableData
    {
        public float MovementSpeed
        {
            get { return ((ShoesData)VisualEquipableItem)._movementSpeed; }
            set { ((ShoesData)VisualEquipableItem)._movementSpeed = value; }
        }


        public ItemWindowShoesData(ShoesData shoes) : base(shoes)
        {

        }

        public ItemWindowShoesData(VisualEquipableItemData shoes) : base(shoes)
        {

        }

        public override void Draw()
        {
            base.Draw();

            MovementSpeed = EditorGUILayout.FloatField("Movement speed", MovementSpeed);
        }

        public override string GetItemTypeName()
        {
            return "Shoes";
        }
    }
}
