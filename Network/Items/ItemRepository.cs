using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;

namespace NetworkProject.Items
{
    [Serializable]
    public static class ItemRepository
    {
        private static List<ItemData> _items;

        public static ItemData GetItemByIdItem(int idItem)
        {
            return _items.Find(x => x.IdItem == idItem);
        }

        public static void LoadItemsFromResources()
        {
            LoadItemsFromResources(Settings.pathToItemsInResources);
        }

        public static void LoadItemsFromResources(string pathToItemsInResources)
        {
            var textAsset = Resources.Load<TextAsset>(pathToItemsInResources);
            var reader = new StringReader(textAsset.text);
            var serializer = new XmlSerializer(typeof(List<ItemData>));

            List<ItemData> items = (List<ItemData>)serializer.Deserialize(reader);

            _items = items;
        }

        public static void SaveItems(string path, List<ItemData> items)
        {
            var serializer = new XmlSerializer(typeof(List<ItemData>));

            using (var writter = new StreamWriter(path))
            {
                serializer.Serialize(writter, items);
            }
        }
    }
}
