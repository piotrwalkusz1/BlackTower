using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;

namespace NetworkProject.Items.Repository
{
    [Serializable]
    public static class ItemRepository
    {
        private static List<Item> _items;

        public static Item GetItemByIdItem(int idItem)
        {
            return _items.Find(x => x.IdItem == idItem);
        }

        public static void LoadItems()
        {
            LoadItemsFromRepository(Settings.pathToItemsInResources);
        }

        public static void LoadItemsFromRepository(string pathToItemsInResources)
        {
            var textAsset = Resources.Load<TextAsset>(pathToItemsInResources);
            var reader = new StringReader(textAsset.text);
            var serializer = new XmlSerializer(typeof(List<Item>));

            List<Item> items = (List<Item>)serializer.Deserialize(reader);

            _items = items;
        }

        public static void SaveItemsToRepository(string path, List<Item> items)
        {
            var serializer = new XmlSerializer(typeof(List<Item>));

            using (var writter = new StreamWriter(path))
            {
                serializer.Serialize(writter, items);
            }
        }
    }
}
