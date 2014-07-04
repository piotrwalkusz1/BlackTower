﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;


namespace NetworkProject.Monsters
{
    public static class MonsterRepository
    {
        private static List<Monster> _monsters;

        public static void LoadMonstersFromResources()
        {
            LoadMonstersFromResources(Settings.pathToMonstersInResources);
        }

        public static void LoadMonstersFromResources(string pathToMonstersInResources)
        {
            var textAsset = Resources.Load<TextAsset>(pathToMonstersInResources);
            var reader = new StringReader(textAsset.text);
            var serializer = new XmlSerializer(typeof(List<Monster>));

            List<Monster> monsters = (List<Monster>)serializer.Deserialize(reader);

            _monsters = monsters;
        }

        public static void SaveMonsters(string path, List<Monster> monsters)
        {
            var serializer = new XmlSerializer(typeof(List<Monster>));

            using (var writter = new StreamWriter(path))
            {
                serializer.Serialize(writter, monsters);
            }
        }
    }
}
