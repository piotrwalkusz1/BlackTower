using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Items.Repository
{
    [Serializable]
    public class VisualItem : Item
    {
        public override int IdItem
        {
            get
            {
                return ItemData.IdItem;
            }
            set
            {
                ItemData.IdItem = value;
            }
        }
        public virtual Item ItemData 
        {
            get
            {
                return _itemData;
            }
        }
        public int IdTexture { get; set; }
        public int IdPrefabOnScene { get; set; }

        private Item _itemData;

        public VisualItem()
        {

        }

        public VisualItem(Item itemData)
        {
            _itemData = itemData;
        }

        public VisualItem(Item itemData, int idTexture, int idPrefabOnScene)
        {
            _itemData = itemData;
            IdTexture = idTexture;
            IdPrefabOnScene = idPrefabOnScene;
        }
    }
}
