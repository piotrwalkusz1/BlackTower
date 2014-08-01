using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using NetworkProject;
using NetworkProject.Items;
using NetworkProject.Spells;
using NetworkProject.Monsters;
using NetworkProject.Buffs;
using UnityEngine;

namespace EditorExtension
{
    public static class EditorSaveLoad
    {
        public static readonly string ITEMS_CLIENT = Application.dataPath + "/Resources/items.txt";
        public static readonly string MONSTERS_CLIENT = Application.dataPath + "/Resources/monsters.txt";
        public static readonly string SPELLS_CLIENT = Application.dataPath + "/Resources/spells.txt";
        public static readonly string BUFFS_CLIENT = Application.dataPath + "/Resources/buffs.txt";

        private static readonly string ITEMS_SERVER = "C:/Users/Piotr/Source/Repos/NetworkProject/NetworkProject-Server/Assets/Resources/items.txt";
        private static readonly string MONSTERS_SERVER = "C:/Users/Piotr/Source/Repos/NetworkProject/NetworkProject-Server/Assets/Resources/monsters.txt";
        private static readonly string SPELLS_SERVER = "C:/Users/Piotr/Source/Repos/NetworkProject/NetworkProject-Server/Assets/Resources/spells.txt";
        private static readonly string BUFFS_SERVER = "C:/Users/Piotr/Source/Repos/NetworkProject/NetworkProject-Server/Assets/Resources/buffs.txt";

        public static void SaveMonsters(List<MonsterMultiData> monsters)
        {
            var monstersInClient = from monster in monsters
                                   select monster.MonsterClientVersion;

            Save(monstersInClient.ToList().ConvertAll<MonsterData>(x => (MonsterData)x).ToArray(), MONSTERS_CLIENT);

            var monstersInServer = from monster in monsters
                                   select monster.MonsterServerVersion;

            Save(monstersInServer.ToList().ConvertAll<MonsterData>(x => (MonsterData)x).ToArray(), MONSTERS_SERVER);
        }

        public static void SaveItems(List<VisualItemData> items)
        {
            Save(items.ToArray(), ITEMS_CLIENT);

            var itemsInServer = from item in items
                                select item.ItemData;

            Save(itemsInServer.ToArray(), ITEMS_SERVER);
        }

        public static void SaveSpells(List<VisualSpellData> spells)
        {
            Save(spells.ToArray(), SPELLS_CLIENT);

            var spellsInServer = from spell in spells
                                 select new SpellData(spell);

            Save(spellsInServer.ToArray(), SPELLS_SERVER);
        }

        public static void SaveBuffs(List<BuffMultiData> buffs)
        {
            var buffsInClient = from buff in buffs
                                select buff.GetClientVersion();

            Save(buffsInClient.ToList().ConvertAll<BuffData>(x => (BuffData)x).ToArray(), BUFFS_CLIENT);

            var buffsInServer = from buff in buffs
                                select buff.GetServerVersion();

            Save(buffsInServer.ToList().ConvertAll<BuffData>(x => (BuffData)x).ToArray(), BUFFS_SERVER);
        }

        public static List<MonsterMultiData> LoadMonsters()
        {
            try
            {
                var monstersInClient = (MonsterData[])Load(MONSTERS_CLIENT);

                var monstersInServer = (MonsterData[])Load(MONSTERS_SERVER);

                var monsters = new List<MonsterMultiData>();

                for (int i = 0; i < monstersInClient.Length; i++)
                {
                    monsters.Add(new MonsterMultiData((VisualMonsterData)monstersInClient[i], (MonsterFullData)monstersInServer[i]));
                }

                return monsters;
            }
            catch (Exception ex)
            {
                MonoBehaviour.print(ex.Message + '\n' + ex.StackTrace);

                return new List<MonsterMultiData>();
            }
        }

        public static List<VisualItemData> LoadItems()
        {
            return new List<VisualItemData>((VisualItemData[])Load(ITEMS_CLIENT));
        }

        public static List<VisualSpellData> LoadSpells()
        {
            return new List<VisualSpellData>((VisualSpellData[])Load(SPELLS_CLIENT));
        }

        public static List<BuffMultiData> LoadBuffs()
        {
            try
            {
                var buffsClient = (BuffData[])Load(BUFFS_CLIENT);

                var buffsServer = (BuffData[])Load(BUFFS_SERVER);

                var buffs = new List<BuffMultiData>();

                for (int i = 0; i < buffsClient.Length; i++)
                {
                    buffs.Add(new BuffMultiData((BuffVisualData)buffsClient[i], (BuffFullData)buffsServer[i]));
                }

                return buffs;
            }
            catch (Exception ex)
            {
                MonoBehaviour.print(ex.Message + '\n' + ex.StackTrace);

                return new List<BuffMultiData>();
            }
        }

        public static void Save(object obj, string path)
        {
            var serializer = new BinaryFormatter();

            using (var stream = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(stream, obj);
            }
        }

        public static object Load(string path)
        {
            var serializer = new BinaryFormatter();

            using (var stream = new FileStream(path, FileMode.Open))
            {
                return serializer.Deserialize(stream);
            }
        }
    }
}
