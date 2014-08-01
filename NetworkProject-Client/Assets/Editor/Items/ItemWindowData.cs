using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using Standard;
using NetworkProject.Items;

namespace EditorExtension
{
    public class ItemWindowData
    {
        public virtual VisualItemData VisualItem { get; protected set; }

        public string[] NameInAllLanguages { get; set; }

        public int IdItem
        {
            get { return VisualItem.ItemData.IdItem; }
            set { VisualItem.ItemData.IdItem = value; }
        }
        public int IdTexture
        {
            get { return VisualItem.IdTexture; }
            set { VisualItem.IdTexture = value; }
        }
        public int IdPrefabOnScene
        {
            get { return VisualItem.IdPrefabOnScene; }
            set { VisualItem.IdPrefabOnScene = value; }
        }

        private bool _isNameInAllLanguagesActive;

        public ItemWindowData(ItemData item) : this(new VisualItemData(item))
        {

        }

        public ItemWindowData(VisualItemData item)
	    {
            VisualItem = item;
	    }

        public virtual void Draw()
        {
            VisualItem.IdItem = EditorGUILayout.IntField("Id item", VisualItem.IdItem);
            IdTexture = EditorGUILayout.IntField("Id texture", IdTexture);
            IdPrefabOnScene = EditorGUILayout.IntField("Id Prefab on scene", IdPrefabOnScene);
        }

        public virtual string GetItemTypeName()
        {
            return "Item";
        }
    }
}
