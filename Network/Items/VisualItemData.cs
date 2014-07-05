using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Items
{
    [Serializable]
    public class VisualItemData : ItemData
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
        public virtual ItemData ItemData 
        {
            get
            {
                return _itemData;
            }
        }
        public int IdTexture { get; set; }
        public int IdPrefabOnScene { get; set; }

        private ItemData _itemData;

        public VisualItemData()
        {

        }

        public VisualItemData(ItemData itemData)
        {
            _itemData = itemData;
        }

        public VisualItemData(ItemData itemData, int idTexture, int idPrefabOnScene)
        {
            _itemData = itemData;
            IdTexture = idTexture;
            IdPrefabOnScene = idPrefabOnScene;
        }
    }
}
