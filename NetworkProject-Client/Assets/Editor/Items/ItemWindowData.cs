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
        public virtual ItemData Item { get; protected set; }

        public string[] NameInAllLanguages { get; set; }

        public int IdItem
        {
            get { return Item.IdItem; }
            set { Item.IdItem = value; }
        }
        public int IdTexture
        {
            get { return Item.IdTexture; }
            set { Item.IdTexture = value; }
        }
        public int IdPrefabOnScene
        {
            get { return Item.IdPrefabOnScene; }
            set { Item.IdPrefabOnScene = value; }
        }

        private bool _isNameInAllLanguagesActive;

        public ItemWindowData(ItemData item)
        {
            Item = item;
        }

        public virtual void Draw()
        {
            Item.IdItem = EditorGUILayout.IntField("Id item", Item.IdItem);
            IdTexture = EditorGUILayout.IntField("Id texture", IdTexture);
            IdPrefabOnScene = EditorGUILayout.IntField("Id Prefab on scene", IdPrefabOnScene);
        }

        public virtual string GetItemTypeName()
        {
            return "Item";
        }
    }
}
