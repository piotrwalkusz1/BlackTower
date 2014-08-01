using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


namespace NetworkProject.Monsters
{
    public static class MonsterRepository
    {
        private static List<MonsterData> _monsters;

        public static void SetMonstersFromResource(string resourceName)
        {
            _monsters = LoadMonstersFromResourcse(resourceName);
        }

        public static List<MonsterData> LoadMonstersFromResourcse(string resourceName)
        {
            var textAsset = Resources.Load<TextAsset>(resourceName);
            var stream = new MemoryStream(textAsset.bytes);

            return LoadMonstersFromStream(stream);
        }

        public static List<MonsterData> LoadMonstersFromFile(string path)
        {
            using (var stream = new FileStream(path, FileMode.OpenOrCreate))
            {
                return LoadMonstersFromStream(stream);
            }
        }

        public static MonsterData GetMonster(int monsterId)
        {
            return _monsters[monsterId];
        }

        private static List<MonsterData> LoadMonstersFromStream(Stream stream)
        {
            var serializer = new BinaryFormatter();

            return new List<MonsterData>((MonsterData[])serializer.Deserialize(stream));
        }
    }
}
