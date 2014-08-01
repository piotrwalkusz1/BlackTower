using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Items;
using UnityEditor;

namespace EditorExtension
{
    public class ItemWindowHelmetData : ItemWindowEquipableData
    {
        public int Defense
        {
            get { return ((HelmetData)VisualEquipableItem.EquipableItemData)._defense; }
            set { ((HelmetData)VisualEquipableItem.EquipableItemData)._defense = value; }
        }

        public ItemWindowHelmetData(HelmetData helmet) : base(helmet)
        {

        }

        public ItemWindowHelmetData(VisualEquipableItemData helmet) : base(helmet)
        {

        }

        public override void Draw()
        {
            base.Draw();

            Defense = EditorGUILayout.IntField("Defense", Defense);
        }

        public override string GetItemTypeName()
        {
            return "Helmet";
        }
    }
}
