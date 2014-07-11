using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
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

        public static void LoadAndSetItemsFromFile(string path)
        {
            _items = new List<ItemData>(LoadItemsFromFile(path));
        }

        public static ItemData[] LoadItemsFromResources(string name)
        {
            var textAsset = Resources.Load<TextAsset>(name);

            if (textAsset == null)
            {
                return new ItemData[0];
            }
            else
            {
                try
                {
                    var stream = new MemoryStream(textAsset.bytes);

                    return LoadItemsFromStream(stream);
                }
                catch
                {
                    MonoBehaviour.print("Plik ma niewłaściwą zawartość. Zostanie utworzona pusta kolekcja.");

                    return new ItemData[0];
                }
            }
            
        }

        public static ItemData[] LoadItemsFromFile(string path)
        {
            try
            {
                using (var reader = new FileStream(path, FileMode.OpenOrCreate))
                {
                    return LoadItemsFromStream(reader);
                }
            }
            catch
            {
                MonoBehaviour.print("Plik nie istnieje lub ma niewłaściwą zawartość. Zostanie utworzona pusta kolekcja.");

                return new ItemData[0];
            }
        }     

        public static void SaveItems(string path, ItemData[] items)
        {
            var serializer = new BinaryFormatter();

            using (var writter = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(writter, items);
            }
        }

        private static ItemData[] LoadItemsFromStream(Stream stream)
        {
            var serializer = new BinaryFormatter();

            return (ItemData[])serializer.Deserialize(stream);
        }
    }
}
